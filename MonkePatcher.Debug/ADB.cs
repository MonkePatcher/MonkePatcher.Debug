using System.Diagnostics;

namespace MonkePatcher.Debug
{
    public class ADB
    {
        public static void OpenLogcatWindow()
            => Process.Start($"cmd.exe {Path.Combine(SDKInstaller.SDKBinPath, "adb.exe")} logcat");
    }
}
