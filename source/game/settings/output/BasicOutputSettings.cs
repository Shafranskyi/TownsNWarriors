﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using taw.game.basicInterfaces;
using taw.game.output;

namespace taw.game.settings.output {
	class BasicOutputSettings : settings.SettinsSetter {
		public override void SetSettings(Settingable obj) {
			BasicOutput output = obj as BasicOutput;
			if (output == null)
				throw new ApplicationException("Wrong generator in BasicOutputSettings.SetSettings");

		}

		protected override void LoadSettingsFromFile() {
			throw new NotImplementedException();
		}
	}
}
