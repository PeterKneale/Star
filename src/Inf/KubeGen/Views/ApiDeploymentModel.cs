namespace KubeGen
{
    public class ApiDeploymentModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string ComponentName { get { return $"{Name}-api-pod"; } }
        public string MetaDataName { get { return $"{Name}-api-deploy"; } }
        
        public int DatabasePort { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseDbName { get; set; }
        public string DatabaseUserName { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseHost { get { return $"{DatabaseName}-service"; } }
    }
}
