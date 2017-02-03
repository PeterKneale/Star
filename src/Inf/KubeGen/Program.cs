using System;

namespace KubeGen
{
    public class Program
    {
        public static void Main()
        {
            Files.Clean();
            
            string[] services = { "account", "user", "member" };
            string[] environments = { "dev", "qa", "prod" };

            foreach (var environment in environments)
            {
                var databaseManifest = Manifests.BuildDatabaseDeploymentManifest(environment);
                Files.Write(environment,"database-deployment", databaseManifest);

                foreach (var service in services)
                {
                    var serviceManifest = Manifests.BuildServiceDeploymentManifest(service, environment);
                    Files.Write(environment, $"{service}-service-deployment", serviceManifest);
                }
            }
            
        }
    }
}
