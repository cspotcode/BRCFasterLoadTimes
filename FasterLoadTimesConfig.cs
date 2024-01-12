using BepInEx.Configuration;

namespace Cspotcode.FasterLoadTimes;

public class FasterLoadTimesConfig {
    public static FasterLoadTimesConfig Instance = null;

    public ConfigEntry<bool> DisableExtendedLoadingTime;

    public FasterLoadTimesConfig(ConfigFile file) {
        var section = "General";
        DisableExtendedLoadingTime = file.Bind(
            section,
            nameof(DisableExtendedLoadingTime),
            true,
            "Disable BRC's \"extended loading time\" which shows the loading screen for an additional half a second to mask pop-in."
        );
    }
}
