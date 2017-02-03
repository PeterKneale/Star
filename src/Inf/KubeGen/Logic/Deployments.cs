using System;

namespace KubeGen
{
    public class Deployments
    {
        public static string GetDeployment(string service, string environment)
        {
            var serviceScopeFactory = Generator.InitializeServices();
            var model = GetDeploymentModel(service, environment);
            var content = Generator.RenderViewAsync(serviceScopeFactory, "Deployment", model).Result;
            return content;
        }
        static DeploymentModel GetDeploymentModel(string service, string environment)
        {
            var model = new DeploymentModel
            {
                Name = service,
                Image = Registry.GetImage(service, environment),
                Database = new DatabaseModel { }
            };
            return model;
        }
    }
}
