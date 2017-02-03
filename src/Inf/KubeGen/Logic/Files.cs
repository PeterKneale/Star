using System;

namespace KubeGen
{
    public class Files
    {
        public static void WriteFile(string service, string environment, string type, string content)
        {
            var directory = "..\\Kubernetes";
            if(!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            var environmentDirectory = $"{directory}\\{environment}";
            if(!System.IO.Directory.Exists(environmentDirectory))
            {
                System.IO.Directory.CreateDirectory(environmentDirectory);
            }
            var fileName = $"{environmentDirectory}\\{service}-{type}.yaml";
            System.IO.File.WriteAllText(fileName, content);
            Console.WriteLine($"Generated: {fileName}");
        }
    }
}
