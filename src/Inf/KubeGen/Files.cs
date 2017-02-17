using System;

namespace KubeGen
{
    public class Files
    {
        const string RootOutputDir = "..\\Kubernetes";
        static Files()
        {
            if (!System.IO.Directory.Exists(RootOutputDir))
            {
                System.IO.Directory.CreateDirectory(RootOutputDir);
            }
        }
        public static void Write(string environment, string name, string content)
        {
            var output = $"{RootOutputDir}\\{environment}";
            if (!System.IO.Directory.Exists(output))
            {
                System.IO.Directory.CreateDirectory(output);
            }

            var file = $"{output}\\{name}.yaml";
            System.IO.File.WriteAllText(file, content);
            Console.WriteLine($"Generated: {file}");
        }
        public static void Clean()
        {
            Console.WriteLine($"Cleaning: {RootOutputDir}");
            System.IO.Directory.Delete(RootOutputDir, true);
        }
    }
}
