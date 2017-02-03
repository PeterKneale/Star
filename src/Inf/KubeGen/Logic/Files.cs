using System;

namespace KubeGen
{
    public class Files
    {
        const string OutputDir = "..\\Kubernetes";

        public static void Write(string environment, string name, string content)
        {
            var environmentDirectory = $"{OutputDir}\\{environment}";

            var output = $"{environmentDirectory}\\{name}.yaml";

            System.IO.File.WriteAllText(output, content);
            Console.WriteLine($"Generated: {output}");
        }
        private static void EnsureDirExists(string directory)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
        }
        public static void Clean()
        {
            System.IO.Directory.Delete(OutputDir, true);
        }
        private static string GetOutputDirectory(string environment)
        {
            var output = $"{OutputDir}\\{environment}";
            EnsureDirExists(OutputDir);
            EnsureDirExists(output);
            return output;
        }
    }
}
