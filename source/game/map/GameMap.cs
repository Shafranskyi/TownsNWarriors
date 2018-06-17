﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using taw.game;
using taw.game.settings;

using taw.game.city;
using taw.game.unit;
using taw.game.controlable.botControl;

namespace taw.game.map {
	public partial class GameMap {
		//---------------------------------------------- Fields ----------------------------------------------
		int sizeX, sizeY;

		List<List<GameCell>> map;
		List<BasicCity> sities;
		List<BasicUnit> units;

		//---------------------------------------------- Properties ----------------------------------------------
		public List<BasicCity> Sities { get => sities; set => sities = value; }
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

			sities = new List<BasicCity>();
			units = new List<BasicUnit>();

			BasicCity.gameMap = this;
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
		}

		public void SendWarriors(List<BasicCity> from, BasicCity to) {
            foreach (var i in from)
				SendWarriors(i, to);
		}

    	public void SendWarriors(BasicCity from, BasicCity to) {
			if (from == to)
				return;

			var unit = from.SendUnit(to);

			if (unit== null)
				return;

			units.Add(unit);
		}
	}
}
