using System;

namespace KubeGen
{
    public class Manifests
    {
        public static string BuildServiceDeploymentManifest(string service, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetServiceDeploymentModel(service, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "ServiceDeployment", model).Result;
            return content;
        }
        public static string BuildDatabaseDeploymentManifest(string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetDatabaseDeploymentModel(environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "DatabaseDeployment", model).Result;
            return content;
        }
        static ServiceDeploymentModel GetServiceDeploymentModel(string service, string environment)
        {
            var model = new ServiceDeploymentModel
            {
                Name = service,
                Image = Registry.GetImage(service, environment),
                Database = new DatabaseConnectionModel{
                    Name = Config.GetDatabaseName(environment),
                    Port = Config.GetDatabasePort(environment),
                    UserName = Config.GetDatabaseUserName(environment),
                    Password = Config.GetDatabasePassword(environment)
                }
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
