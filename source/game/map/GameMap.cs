﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TownsAndWarriors.game;
using TownsAndWarriors.game.settings;

using TownsAndWarriors.game.sity;
using TownsAndWarriors.game.unit;
using TownsAndWarriors.game.bot;

namespace TownsAndWarriors.game.map {
	public partial class GameMap {
		//---------------------------------------------- Fields ----------------------------------------------
		int sizeX, sizeY;

		List<List<GameCell>> map;
		List<BasicSity> sities;
		List<BasicUnit> units;

		List<BasicBot> bots;

		//---------------------------------------------- Properties ----------------------------------------------
		public List<BasicSity> Sities { get => sities; set => sities = value; }
		public List<BasicUnit> Units => units;
		public List<List<GameCell>> Map => map;
		public int SizeX => sizeX;
		public int SizeY => sizeY;

		//---------------------------------------------- Ctor ----------------------------------------------
		public GameMap(int SizeX, int SizeY) {
			sizeX = SizeX; sizeY = SizeY;
			map = new List<List<GameCell>>(sizeY);
			for (int i = 0; i < sizeY; ++i) {
				map.Add(new List<GameCell>(sizeX));
				for (int j = 0; j < sizeX; ++j) 
					map[i].Add(new GameCell());
			}

			sities = new List<BasicSity>();
			units = new List<BasicUnit>();
			bots = new List<BasicBot>();

			BasicSity.gameMap = this;
		}

		//---------------------------------------------- Methods ----------------------------------------------
		public void Tick() {
			foreach (var sity in sities)
				sity.TickReact();

			REPEAT_UNITS_TURN:
			foreach (var unit in units) {
				if (unit.TickReact())
					goto REPEAT_UNITS_TURN;
			}

			foreach (var bot in bots) 
				if(bot != null)
					bot.TickReact();
			//System.Windows.MessageBox.Show("map:" + game.globalGameInfo.tick.ToString());
		}

		public void SendWarriors(List<BasicSity> from, BasicSity to) {
            foreach (var i in from)
				SendWarriors(i, to);
		}

    	public void SendWarriors(BasicSity from, BasicSity to) {
			if (from == to)
				return;

			var unit = from.SendUnit(to);

			if (unit== null)
				return;

			unit.SetCanvas(canvas);

			unit.InitializeShape();

			units.Add(unit);
		}

		public void SetBotsSize(int size) {
			for (int i = 0; i < size; ++i)
				bots.Add(null);
		}

		public void SetBot(int id, BasicBot type) {
			bots[id] = type;
		}
	}
}
