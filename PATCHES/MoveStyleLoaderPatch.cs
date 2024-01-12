using HarmonyLib;
using Reptile;
using UnityEngine;

namespace Cspotcode.FasterLoadTimes.Patches;

[HarmonyPatch(typeof(MoveStyleLoader))]
class MoveStyleLoaderPatch {
    private static bool loaded = false;
    
    private static Texture[,] loadedMoveStyleTextures;

    [HarmonyPrefix]
    [HarmonyPatch(nameof(MoveStyleLoader.LoadAssetsASync))]
    private static bool LoadAssetsASync_Patch(MoveStyleLoader __instance, AssetsToLoadData assetsToLoadData) {
        if(assetsToLoadData.loadCharacters) {
            if(loaded) {
                Logger.Log(() => $"restoring {loadedMoveStyleTextures}");
                __instance.loadedMoveStyleTextures = loadedMoveStyleTextures;
                for(var i = 0; i < loadedMoveStyleTextures.GetLength(0); i++) {
                    for(var j = 0; j < loadedMoveStyleTextures.GetLength(1); j++) {
                        Logger.Log(() => loadedMoveStyleTextures[i, j]);
                    }
                }
                return false;
            } else {
                loaded = true;
            }
        }
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(MoveStyleLoader.UnloadAssets))]
    private static bool UnloadAssets_Prefix(MoveStyleLoader __instance) {
        Logger.Log(() => $"{loaded} {__instance.loadedMoveStyleTextures}");
        if(loaded && __instance.loadedMoveStyleTextures[1, 0] != null) {
            loadedMoveStyleTextures = __instance.loadedMoveStyleTextures;
            for(var i = 0; i < loadedMoveStyleTextures.GetLength(0); i++) {
                for(var j = 0; j < loadedMoveStyleTextures.GetLength(1); j++) {
                    Logger.Log(() => loadedMoveStyleTextures[i, j]);
                }
            }
            return false;
        }
        return true;
    }
}