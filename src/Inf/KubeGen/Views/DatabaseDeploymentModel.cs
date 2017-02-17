namespace KubeGen
{
    public class DatabaseDeploymentModel
    {
        public string Image { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ComponentName { get { return $"{Name}-db-pod"; } }
        public string MetaDataName { get { return $"{Name}-db-deploy"; } }
    }


}
