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
            var model = GetApiServiceModel(service, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "api-service", model).Result;
            return content;
        }
        public static string ConstructApiIngress(string service, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetApiIngressModel(service, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "api-ingress", model).Result;
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
                Image = Registry.GetImage(api, environment),
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
        static ApiServiceModel GetApiServiceModel(string api, string environment)
        {
            var model = new ApiServiceModel
            {
                Name = api
            };
            return model;
        }
        static ApiIngressModel GetApiIngressModel(string api, string environment)
        {
            var model = new ApiIngressModel
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
