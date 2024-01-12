using System.Collections;
using System.IO;
using HarmonyLib;
using Reptile;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Cspotcode.FasterLoadTimes.Patches;

[HarmonyPatch(typeof(Assets))]
internal static class AssetsPatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Assets.LoadBundleASync))]
    private static bool LoadBundleASync_Prefix(ref IEnumerator __result, Assets __instance, Bundle bundleToLoad)
    {
        if(bundleToLoad.IsLoaded) return false;
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Assets.LoadAssetLoadersForSceneASync))]
    public static bool LoadAssetLoadersForSceneASync_Prefix(Assets __instance, ref IEnumerator __result, string sceneName)
    {
        __result = LoadAssetLoadersForSceneASync(__instance, sceneName);
        return false;
    }

    private static IEnumerator LoadAssetLoadersForSceneASync(Assets __instance, string sceneName) {
        AssetsToLoadData assetsToLoadData = __instance.GetAssetsToLoadDataForScene(sceneName);
        List<ISceneAssetLoader> list = __instance.RetrieveAllAssetLoaders();
        __instance.assetLoadersToLoad = list.Count;
        foreach (ISceneAssetLoader item in list)
        {
#if DEBUG
            Logger.Log(() => $"Starting {item.GetType().Name}");
            var s = new Stopwatch();
            s.Start();
#endif
            yield return item.LoadAssetsASync(assetsToLoadData);
#if DEBUG
            s.Stop();
            Logger.Log(() => $"Finished {item.GetType().Name} in {s.Elapsed}");
#endif
            __instance.assetLoadersLoaded++;
        }
    }
}