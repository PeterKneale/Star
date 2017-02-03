using System;

namespace KubeGen
{
    public class Program
    {
        public static void Main()
        {
            Files.Clean();

            string[] apis = { "account", "user", "member" };
            string[] environments = { "dev", "qa", "prod" };

            foreach (var environment in environments)
            {
                var databaseManifest = Manifests.BuildDatabaseDeploymentManifest(environment);
                Files.Write(environment,"database-deployment", databaseManifest);

                foreach (var api in apis)
                {
                    var apiDeployment = Manifests.ConstructApiDeployment(api, environment);
                    var apiService = Manifests.ConstructApiService(api, environment);
                    var apiIngress = Manifests.ConstructApiIngress(api, environment);
                    Files.Write(environment, $"{api}-deployment", apiDeployment);
                    Files.Write(environment, $"{api}-service", apiService);
                    // TODO: Not sure whether these are necessary
                    //Files.Write(environment, $"{api}-ingress", apiIngress);
                }
            }
            
        }
    }
}
