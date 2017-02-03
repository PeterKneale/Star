// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace KubeGen
{
    public class ServiceDeploymentModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string ComponentName { get { return $"{Name}-api-pod"; } }
        public string MetaDataName { get { return $"{Name}-api-deploy-pod"; } }

        public DatabaseConnectionModel Database{ get; set; }
    }
    public class DatabaseConnectionModel
    {
        public int Port { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get { return $"{Name}-service"; } }
    }

    public class DatabaseDeploymentModel
    {
        public string Image { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ComponentName { get { return $"{Name}-api-pod"; } }
        public string MetaDataName { get { return $"{Name}-api-deploy-pod"; } }
    }
}
