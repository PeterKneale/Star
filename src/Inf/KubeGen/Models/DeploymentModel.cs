// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace KubeGen
{
    public class ApiDeploymentModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string ComponentName { get { return $"{Name}-api-pod"; } }
        public string MetaDataName { get { return $"{Name}-api-deploy"; } }

        public DatabaseConnectionModel Database { get; set; }
    }
    public class ApiServiceModel
    {
        public string Name { get; set; }

        public string MetaDataName { get { return $"{Name}-svc"; } }
        public string ComponentName { get { return $"{Name}-svc"; } }
        public string Selector { get { return $"{Name}-api-pod"; } }
    }
    public class ApiIngressModel
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Host { get { return $"{Name}.{Domain}"; } }
        public string MetaDataName { get { return $"{Name}-ingress"; } }
        public string ComponentName { get { return $"{Name}-ingress"; } }
        public string Selector { get { return $"{Name}-api-svc"; } }
    }
    public class DatabaseConnectionModel
    {
        public int Port { get; set; }
        public string Name { get; set; }
        public string DbName { get; set; }
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
        public string ComponentName { get { return $"{Name}-db-pod"; } }
        public string MetaDataName { get { return $"{Name}-db-deploy"; } }
    }


}
