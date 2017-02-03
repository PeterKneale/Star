// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace KubeGen
{
    public class DeploymentModel
    {
        public string Name { get; set; }
        public string ComponentName { get { return $"{Name}-api-pod"; } }
        public string MetaDataName { get { return $"{Name}-api-deploy-pod"; } }
        public string Image { get; set; }
        public DatabaseModel Database { get; set; }
    }

    public class DatabaseModel
    {

        public string Host { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
