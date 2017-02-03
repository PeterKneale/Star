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
                
                var gatewayDeployment = Manifests.ConstructGatewayDeployment("gateway", environment);
                Files.Write(environment, $"gateway-deployment", gatewayDeployment);
                var gatewayService = Manifests.ConstructGatewayService("gateway", environment);
                Files.Write(environment, $"gateway-service", gatewayService);
                var gatewayIngress = Manifests.ConstructGatewayIngress("gateway", environment);
                Files.Write(environment, $"gateway-ingress", gatewayIngress);
                
                foreach (var api in apis)
                {
                    var apiDeployment = Manifests.ConstructApiDeployment(api, environment);
                    var apiService = Manifests.ConstructApiService(api, environment);
                    Files.Write(environment, $"{api}-deployment", apiDeployment);
                    Files.Write(environment, $"{api}-service", apiService);
                }
                
            }
            
        }
    }
}
