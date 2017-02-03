using System;

namespace KubeGen
{
    public class Registry
    {
        public static string GetAddress()
        {
            var awsAccount = "123";
            var awsRegion = "ap-southeast-2";
            var registry = $"{awsAccount}.dkr.ecr.{awsRegion}.amazonaws.com";
            return registry;
        }
        public static string GetImage(string imageName, string imageTag)
        {
            var registry = GetAddress();
            var image = $"{registry}/{imageName}:{imageTag}";
            //return image;
            return "tutum/hello-world";

        }
    }
}
