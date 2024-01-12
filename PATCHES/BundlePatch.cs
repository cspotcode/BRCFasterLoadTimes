using System;
using System.Runtime.InteropServices.WindowsRuntime;
using HarmonyLib;
using Reptile;
using UnityEngine;

namespace Cspotcode.FasterLoadTimes.Patches;

[HarmonyPatch(typeof(Bundle))]
class BundlePatch {

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Bundle.SetAssetBundle))]
    private static void SetAssetBundle_Prefix(Bundle __instance, AssetBundle assetBundle) {
        Logger.Log(() => $"{Logger.Timestamp()} Loaded {assetBundle.name}");
    }
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Bundle.Unload))]
    private static bool Unload_Prefix(Bundle __instance) {
        switch(__instance.Name) {
            case "characters":
            case "character_animation":
                return false;
        }
        Logger.Log(() => $"{Logger.Timestamp()} Unloading {__instance.assetBundle.name}");
        return true;
    }
}