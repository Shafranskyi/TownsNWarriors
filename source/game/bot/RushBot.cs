﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TownsAndWarriors.game.settings;


namespace TownsAndWarriors.game.bot {
	public class RushBot : BasicBot {
		//---------------------------------------------- Fields ----------------------------------------------
		List<sity.BasicSity> botSities;

		//---------------------------------------------- Properties ----------------------------------------------


		//---------------------------------------------- Ctor ----------------------------------------------
		public RushBot(game.map.GameMap Map,
			List<game.sity.BasicSity> Sities,
			List<game.unit.BasicUnit> Units,
			byte botId) {
			map = Map;
			sities = Sities;
			units = Units;
			playerId = botId;
		}

		//---------------------------------------------- Methods ----------------------------------------------
		public override bool TickReact() {
			if (globalGameInfo.tick > settings.values.bot_rushBot_Tick_IgnoreFirstN && 
				globalGameInfo.tick % values.bot_rushBot_Tick_React == 0) {
				REPEAT_FILL_CITY:
				foreach (var sity in sities) {
					if (botSities.Contains(sity)) {
						if (sity.playerId != this.playerId) {
							botSities.Remove(sity);
							goto REPEAT_FILL_CITY;
						}
					}
					else if (sity.playerId == this.playerId) {
						botSities.Add(sity);
						goto REPEAT_FILL_CITY;
					}
				}

				return true;
			}

			return false;
		}
	}
}
