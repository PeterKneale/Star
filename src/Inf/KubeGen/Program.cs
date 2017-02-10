using System;

namespace KubeGen
{
    public class Program
    {
        public static void Main()
        {
            Files.Clean();
            var Factory = Generator.InitializeServices();

            string[] apis = { "account", "user", "member" };
            string[] environments = { "dev", "qa", "prod" };

            foreach (var environment in environments)
            {
                Files.Write(environment, "database-deployment", Generator.Render(Factory, "DatabaseDeployment", GetDatabaseDeploymentModel(environment)));
                Files.Write(environment, $"gateway-deployment", Generator.Render(Factory,"GatewayDeployment",  GetGatewayDeploymentModel("gateway", environment)));
                Files.Write(environment, $"gateway-service", Generator.Render(Factory,"GatewayService", GetGatewayServiceModel("gateway")));
                Files.Write(environment, $"gateway-ingress", Generator.Render(Factory,"GatewayIngress", GetGatewayIngressModel("gateway", environment)));

                foreach (var api in apis)
                {
                    Files.Write(environment, $"{api}-deployment", Generator.Render(Factory,"ApiDeployment", GetApiDeploymentModel(api, environment)));
                    Files.Write(environment, $"{api}-service", Generator.Render(Factory,"ApiService",  GetApiServiceModel(api)));
                }
            }
        }

        static DatabaseDeploymentModel GetDatabaseDeploymentModel(string environment)
        {
            var model = new DatabaseDeploymentModel
            {
                Image = "postgres",
                Name = Config.GetDatabaseName(environment),
                Port = Config.GetDatabasePort(environment),
                UserName = Config.GetDatabaseUserName(environment),
                Password = Config.GetDatabasePassword(environment)
            };
            return model;
        }
        static ApiDeploymentModel GetApiDeploymentModel(string api, string environment)
        {
            var model = new ApiDeploymentModel
            {
                Name = api,
                Image = Config.GetAmiImage(api, environment),
                DatabaseName = Config.GetDatabaseName(environment),
                DatabasePort = Config.GetDatabasePort(environment),
                DatabaseUserName = Config.GetDatabaseUserName(environment),
                DatabasePassword = Config.GetDatabasePassword(environment)
            };
            return model;
        }
        static ApiServiceModel GetApiServiceModel(string api)
        {
            return new ApiServiceModel { Name = api };
        }
        static GatewayDeploymentModel GetGatewayDeploymentModel(string name, string environment)
        {
            return new GatewayDeploymentModel { Name = name, Image = Config.GetGatewayImage(name, environment) };
        }
        static GatewayServiceModel GetGatewayServiceModel(string api)
        {
            return new GatewayServiceModel { Name = api };
        }
        static GatewayIngressModel GetGatewayIngressModel(string api, string environment)
        {
            return new GatewayIngressModel { Name = api, Domain = Config.GetDomain(environment) };
        }
    }
}
