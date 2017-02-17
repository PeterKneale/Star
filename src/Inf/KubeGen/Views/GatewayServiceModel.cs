namespace KubeGen
{
    public class GatewayServiceModel
    {
        public string Name { get; set; }

        public string MetaDataName { get { return $"{Name}-svc"; } }
        public string ComponentName { get { return $"{Name}-svc"; } }
        public string Selector { get { return $"{Name}-gateway-pod"; } }
    }


}
