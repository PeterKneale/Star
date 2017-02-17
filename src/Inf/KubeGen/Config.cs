using System;

namespace KubeGen
{
    public class Config
    {
        public static string GetDatabaseName(string environment)
        {
            return $"star";
        }
        public static string GetDatabaseUserName(string environment)
        {
            return $"star{environment}user";
        }
        public static string GetDatabasePassword(string environment)
        {
            return $"star{environment}password";
        }

        public static int GetDatabasePort(string environment)
        {
            return 5432;
        }
        public static string GetDomain(string environment)
        {
            return $"{environment}.kube.nearmapdev.com";
        }
        public static string GetAddress()
        {
            var awsAccount = "123";
            var awsRegion = "ap-southeast-2";
            var registry = $"{awsAccount}.dkr.ecr.{awsRegion}.amazonaws.com";
            return registry;
        }
        public static string GetAmiImage(string imageName, string imageTag)
        {
            var registry = GetAddress();
            var image = $"{registry}/{imageName}:{imageTag}";
            //return image;
            return "tutum/hello-world";
        }
        public static string GetGatewayImage(string name, string imageTag)
        {
            return "tutum/hello-world";
        }
    }
}
