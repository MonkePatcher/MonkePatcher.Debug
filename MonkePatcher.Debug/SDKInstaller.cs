using System.IO.Compression;

namespace MonkePatcher.Debug
{
    public class SDKInstaller
    {
        static SDKInstaller()
        {
            string sdkPath = SDKPath;

            // Check for the source.properties file, its small and has the version in it ¯\_(ツ)_/¯
            string sourcePropertiesPath = Path.Combine(sdkPath, "source.properties");
            IsInstalled = File.Exists(sourcePropertiesPath);
        }

        public static bool IsInstalled { get; private set; }

        private static string SDKPath
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MonkePatcher", "Debug", "SDK");
                var dirInfo = new DirectoryInfo(path);

                if (!dirInfo.Exists)
                {
                    Directory.CreateDirectory(path);
                }

                return dirInfo.FullName;
            } 
        }

        public static string SDKBinPath
        {
            get => Path.Combine(SDKPath, "platform-tools");
        }

        public static void Install()
        {
            if (!IsInstalled)
            {
                string zipFile = Path.Combine(SDKPath, "platform-tools.zip");
                // extract zip
                using (var zipStream = AssemblyUtils.GetEmbeddedResource("MonkePatcher.Debug.Resources.platform-tools-windows.zip"))
                {
                    using var stream = File.Create(zipFile);
                    zipStream.CopyTo(stream);
                }

                ZipFile.ExtractToDirectory(zipFile, SDKPath);
            }
        }
    }
}
