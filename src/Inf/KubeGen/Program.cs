using System;

namespace KubeGen
{
    public class Program
    {
        public static void Main()
        {
            string[] services = { "account", "user", "member" };
            string[] environments = { "dev", "qa", "prod" };

            foreach (var service in services)
            {
                foreach (var environment in environments)
                {
                    var deployment = Deployments.GetDeployment(service, environment);
                    Files.WriteFile(service, environment, "deployment", deployment);
                }
            }
        }
    }
}
