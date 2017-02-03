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
    }
}
