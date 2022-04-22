using System.Diagnostics;

namespace MonkePatcher.Debug
{
    public class ADB
    {
        public static void OpenLogcatWindow()
            => Process.Start($"cmd.exe {Path.Combine(SDKInstaller.SDKBinPath, "adb.exe")} logcat");

        public static StreamReader Shell(params string[] args)
        { 
            var process = Process.Start($"{Path.Combine(SDKInstaller.SDKBinPath, "adb.exe")} shell {string.Join(" ", args)}");
            process.BeginOutputReadLine();
            var output = process.StandardOutput;
            return output;
        }
    }
}
