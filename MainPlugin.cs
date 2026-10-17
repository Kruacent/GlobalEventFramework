using Exiled.API.Features;
using System;
using Exiled.API.Enums;
using KE.Utils.API.Displays.DisplayMeow;
using Exiled.API.Interfaces;
using GlobalEventFramework.GEFE.API.Features;

namespace GlobalEventFramework
{
    internal class MainPlugin : Plugin<Config>
	{
        public override string Name => "KE.GEFramework";
        public override string Prefix => "KE.GEF";



		public static readonly HintPlacement GEAnnouncement = new HintPlacement(0, 50, HintServiceMeow.Core.Enum.HintAlignment.Center);
		public static readonly HintPlacement GEEffect = new HintPlacement(360, 300, HintServiceMeow.Core.Enum.HintAlignment.Right);


        internal static MainPlugin Instance { get; private set;}

		public static Config Configs => Instance.Config;

        public override void OnEnabled()
		{
            Instance = this;
            KEEvents.OnEnabled();

        }

		public override void OnDisabled()
		{
			KEEvents.OnDisabled();
            Instance = null;
		}

		


	}
}
