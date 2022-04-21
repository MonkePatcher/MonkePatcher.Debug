using AndroidSdk;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

namespace MonkePatcher.Debug
{
    internal class Global
    {
        
        protected internal static string GorillaTagDir { get => "/sdcard/Android/data/com.AnotherAxiom.GorillaTag/files"; }
        
        protected internal static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().Location;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        protected internal static string DataDirectory
        {
            get
            {
                if (Debugger.IsAttached)
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                        Path.Combine(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), "DebugData")
                        : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DebugData");
                }

                return Path.Combine(AssemblyDirectory, "..", "..", "..", "DebugData");
            }
        }

        private static object sdkLocker = new();

        private static DirectoryInfo? androidSdkHome;

        protected internal static AndroidSdkManager GetAndroidSdk(bool useGlobalSdk = true)
        {
            lock (sdkLocker)
            {
                if (useGlobalSdk)
                {
                    var globalSdk = AndroidSdkManager.FindHome()?.FirstOrDefault();

                    if (globalSdk != null && globalSdk.Exists)
                        androidSdkHome = globalSdk;
                }

                if (androidSdkHome == null || !androidSdkHome.Exists)
                {
                    var sdkPath = Path.Combine(DataDirectory, "android-sdk");

                    if (!Directory.Exists(sdkPath))
                        Directory.CreateDirectory(sdkPath);

                    androidSdkHome = new DirectoryInfo(sdkPath);

                    var s = new AndroidSdkManager(androidSdkHome);

                    s.Acquire();

                    Assert.True(s.SdkManager.IsUpToDate());
                }

                return new AndroidSdkManager(androidSdkHome);
            }
        }
    }
}
