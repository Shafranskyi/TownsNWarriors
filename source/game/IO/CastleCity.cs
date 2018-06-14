﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Animation;

using System.Reflection;


namespace TownsAndWarriors.game.sity
{
   public partial class CastleCity
   {
		public override void InitializeShape()
		{
			shape = new Grid();
			shape.Style = (Style)shape.FindResource("BasicCityStyle");
			FillShape();

			//Delegates
			settings.size.SizeChanged += () => {
				shape.Children.Clear();
				FillShape();
			};

			shape.MouseEnter += (a, b) => {
				double min = settings.size.OneCellSizeX < settings.size.OneCellSizeY ? settings.size.OneCellSizeX : settings.size.OneCellSizeY;

				var anim = new System.Windows.Media.Animation.DoubleAnimation
				{
					From = min * settings.size.sitySizeMult,
					To = min,
					Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100)),
				};
				label.BeginAnimation(Ellipse.WidthProperty, anim);
				label.BeginAnimation(Ellipse.HeightProperty, anim);
			};
			shape.MouseLeave += (a, b) => {
				double min = settings.size.OneCellSizeX < settings.size.OneCellSizeY ? settings.size.OneCellSizeX : settings.size.OneCellSizeY;

				var anim = new System.Windows.Media.Animation.DoubleAnimation
				{
					To = min * settings.size.sitySizeMult,
					From = min,
					Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100)),
				};
				label.BeginAnimation(Ellipse.WidthProperty, anim);
				label.BeginAnimation(Ellipse.HeightProperty, anim);
			};

			shape.MouseRightButtonDown += delegate (object sender, MouseButtonEventArgs e)
			{
				foreach (var x in selected)
				{
					switch (settings.values.style_Num)
					{
						case 0:
							SetUiColor(this.label, this.playerId);
							break;
						case 1:
							SetImgColor(this.label, this.playerId);
							break;
					}
				}
				selected.Clear();
			};

			shape.MouseLeftButtonDown += delegate (object sender, MouseButtonEventArgs e) {
				if (e.ClickCount == 1)
				{
					if (playerId == 1)
					{
						if (selected.Contains(this) == false)
						{
							selected.Add(this);
							label.BorderBrush = settings.colors.citySelectedStroke;
							label.BorderThickness = new Thickness(settings.colors.citySelectedStrokeThickness);
						}
					}
					else if (playerId != 1)
					{
						gameMap.SendWarriors(selected, this);
						foreach (var x in selected)
						{
							switch (settings.values.style_Num)
							{
								case 0:
									SetUiColor(this.label, this.playerId);
									break;
								case 1:
									SetImgColor(this.label, this.playerId);
									break;
							}
						}
						selected.Clear();
					}
				}
				else
				{
					if (playerId == 1)
					{
						gameMap.SendWarriors(selected, this);
						foreach (var x in selected)
						{
							switch (settings.values.style_Num)
							{
								case 0:
									SetUiColor(this.label, this.playerId);
									break;
								case 1:
									SetImgColor(this.label, this.playerId);
									break;
							}
						}
						selected.Clear();
					}
				}
			};
		}
		protected override void FillShape()
		{
			double min = settings.size.OneCellSizeX < settings.size.OneCellSizeY ? settings.size.OneCellSizeX : settings.size.OneCellSizeY;

			label = new Label();
			label.Width = min * settings.size.sitySizeMult;
			label.Height = min * settings.size.sitySizeMult;
			switch (settings.values.style_Num)
			{
				case 0:
					label.Background = settings.colors.neutralTownFill;
					label.Style = (Style)label.FindResource("BaseCityStyle");
					SetUiColor(this.label, this.playerId);
					break;
				case 1:
					label.Style = (Style)label.FindResource("ColorCityStyle1");
					label.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"..\..\img\cities\castle_p0_s4_l5.png", UriKind.Relative)) };
					SetImgColor(this.label, this.playerId);
					break;
			}

			SetUiColor(this.label, this.playerId);
			shape.Children.Add(label);

			text = new Label()
			{
				Foreground = Brushes.Black,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center,
				Width = min * settings.size.sitySizeMult,
				Height = min * settings.size.sitySizeMult,
			};
			shape.Children.Add(text);
		}

		public override void SetImgColor(Label label, byte playerId)
		{
			if (playerId == 1)
			{
				label.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"..\..\img\cities\castle_p1_s4_l5.png", UriKind.Relative)) };
			}
			else if (playerId == 2)
			{
				label.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"..\..\img\cities\castle_p2_s4_l5.png", UriKind.Relative)) };
			}
		}
	}
}
