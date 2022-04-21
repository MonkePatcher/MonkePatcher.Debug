using System.Reflection;

namespace MonkePatcher.Debug
{
    internal class AssemblyUtils
    {
        public static Stream? GetEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            Stream? stream = assembly.GetManifestResourceStream(resourceName);
            return stream;
        }
    }
}
