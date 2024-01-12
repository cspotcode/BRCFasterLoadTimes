using System.Collections;
using HarmonyLib;
using Reptile;

namespace Cspotcode.FasterLoadTimes.Patches;

[HarmonyPatch(typeof(BaseModule))]
class BaseModulePatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(BaseModule.WaitForExtendedLoadingTime))]
    private static bool WaitForExtendedLoadingTime_Prefix(ref IEnumerator __result, BaseModule __instance, ASceneSetupInstruction sceneSetupInstruction) {
        if(FasterLoadTimesConfig.Instance.DisableExtendedLoadingTime.Value) {
            sceneSetupInstruction.extendedLoadingTime = 0.01f;
        }
        return true;
        // __result = WaitForExtendedLoadingTime(__instance, sceneSetupInstruction);
        // return false;
    }

    // private static IEnumerator WaitForExtendedLoadingTime(BaseModule __instance, ASceneSetupInstruction sceneSetupInstruction) {
    //     if (sceneSetupInstruction.IsStage)
    //     {
    //         yield return null;
    //         __instance.HandleStageFullyLoaded();
    //     }
    // }
}