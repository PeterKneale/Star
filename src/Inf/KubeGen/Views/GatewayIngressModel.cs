namespace KubeGen
{
    public class GatewayIngressModel
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Host { get { return $"{Name}.{Domain}"; } }
        public string MetaDataName { get { return $"{Name}-ingress"; } }
        public string ComponentName { get { return $"{Name}-ingress"; } }
        public string Selector { get { return $"{Name}-gateway-svc"; } }
    }


}
