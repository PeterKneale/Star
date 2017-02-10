namespace KubeGen
{
    public class GatewayDeploymentModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string ComponentName { get { return $"{Name}-gateway-pod"; } }
        public string MetaDataName { get { return $"{Name}-gateway-deploy"; } }
    }


}
