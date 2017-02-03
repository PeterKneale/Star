using System;

namespace KubeGen
{
    public class Files
    {
        const string RootOutputDir = "..\\Kubernetes";

        public static void Write(string environment, string name, string content)
        {
            var environmentDirectory = GetOutputDirectory(environment);
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
            if (System.IO.Directory.Exists(RootOutputDir))
            {
                Console.WriteLine($"Cleaning: {RootOutputDir}");
                System.IO.Directory.Delete(RootOutputDir, true);
            }
        }
        private static string GetOutputDirectory(string environment)
        {
            var output = $"{RootOutputDir}\\{environment}";
            EnsureDirExists(RootOutputDir);
            EnsureDirExists(output);
            return output;
        }
    }
}
