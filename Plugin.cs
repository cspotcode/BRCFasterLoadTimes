using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Cspotcode.FasterLoadTimes.Patches;
using Reptile;

namespace Cspotcode.FasterLoadTimes
{

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class FasterLoadTimesPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.cspotcode.FasterLoadTimes";
        private const string PluginName = "FasterLoadTimes";
        private const string VersionString = "1.0.0";

        private Harmony harmony;
        public static FasterLoadTimesPlugin Instance {get; private set;}
        public new ManualLogSource Logger => base.Logger;

        private void Awake()
        {
            Instance = this;
            harmony = new Harmony(MyGUID);
            FasterLoadTimesConfig.Instance = new FasterLoadTimesConfig(Config);
            harmony.PatchAll();
        }
    }
}
