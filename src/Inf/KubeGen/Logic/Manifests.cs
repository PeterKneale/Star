using System;

namespace KubeGen
{
    public class Manifests
    {
        public static string ConstructApiDeployment(string service, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetApiDeploymentModel(service, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "api-deployment", model).Result;
            return content;
        }
        public static string ConstructApiService(string service, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetApiServiceModel(service);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "api-service", model).Result;
            return content;
        }
        public static string ConstructGatewayDeployment(string name, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetGatewayDeploymentModel(name, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "gateway-deployment", model).Result;
            return content;
        }
        public static string ConstructGatewayService(string name, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetGatewayServiceModel(name);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "gateway-service", model).Result;
            return content;
        }
        public static string ConstructGatewayIngress(string service, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetGatewayIngressModel(service, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "gateway-ingress", model).Result;
            return content;
        }
        public static string BuildDatabaseDeploymentManifest(string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetDatabaseDeploymentModel(environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "DatabaseDeployment", model).Result;
            return content;
        }
        static ApiDeploymentModel GetApiDeploymentModel(string api, string environment)
        {
            var model = new ApiDeploymentModel
            {
                Name = api,
                Image = Registry.GetAmiImage(api, environment),
                Database = new DatabaseConnectionModel
                {
                    Name = Config.GetDatabaseName(environment),
                    Port = Config.GetDatabasePort(environment),
                    UserName = Config.GetDatabaseUserName(environment),
                    Password = Config.GetDatabasePassword(environment)
                }
            };
            return model;
        }
        static ApiServiceModel GetApiServiceModel(string api)
        {
            var model = new ApiServiceModel
            {
                Name = api
            };
            return model;
        }
        static GatewayDeploymentModel GetGatewayDeploymentModel(string name, string environment)
        {
            var model = new GatewayDeploymentModel
            {
                Name = name,
                Image = Registry.GetGatewayImage(name, environment),
            };
            return model;
        }
        static GatewayServiceModel GetGatewayServiceModel(string api)
        {
            var model = new GatewayServiceModel
            {
                Name = api
            };
            return model;
        }
        static GatewayIngressModel GetGatewayIngressModel(string api, string environment)
        {
            var model = new GatewayIngressModel
            {
                Name = api,
                Domain = Config.GetDomain(environment)
            };
            return model;
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
    }
}
