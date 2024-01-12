using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BepInEx;

namespace Cspotcode.FasterLoadTimes;
class Logger {

    public delegate object GetMessageDelegate();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Log(GetMessageDelegate fn) {
#if DEBUG
        FasterLoadTimesPlugin.Instance.Logger.LogInfo(fn());
#endif
    }
    public static string Timestamp() {
        return DateTimeOffset.Now.ToString("HH:mm:ss.ffff");
    }
}
