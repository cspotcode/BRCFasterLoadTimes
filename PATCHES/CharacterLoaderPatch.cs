using HarmonyLib;
using Reptile;
using UnityEngine;

namespace Cspotcode.FasterLoadTimes.Patches;

[HarmonyPatch(typeof(CharacterLoader))]
class CharacterLoaderPatch {
    private static bool loaded = false;
    private static Material[,] loadedCharacterMaterials;
    private static GameObject[] loadedCharacterFbxAssets;

    [HarmonyPrefix]
    [HarmonyPatch(nameof(CharacterLoader.LoadAssetsASync))]
    private static bool LoadAssetsASync_Patch(CharacterLoader __instance, AssetsToLoadData assetsToLoadData) {
        if(assetsToLoadData.loadCharacters) {
            if(loaded) {
                __instance.loadedCharacterMaterials = loadedCharacterMaterials;
                __instance.loadedCharacterFbxAssets = loadedCharacterFbxAssets;
                return false;
            } else {
                loaded = true;
            }
        }
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(CharacterLoader.UnloadAssets))]
    private static bool UnloadAssets_Prefix(CharacterLoader __instance) {
        if(loaded && __instance.loadedCharacterFbxAssets[0] != null) {
            loadedCharacterMaterials = __instance.loadedCharacterMaterials;
            loadedCharacterFbxAssets = __instance.loadedCharacterFbxAssets;
            return false;
        }
        return true;
    }
}