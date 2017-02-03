using System;

namespace KubeGen
{
    public class Config
    {
        public static string GetDatabaseName(string environment)
        {
            return $"db-{environment}";
        }
        public static string GetDatabaseUserName(string environment)
        {
            return $"dbuser-{environment}";
        }
        public static string GetDatabasePassword(string environment)
        {
            return $"dbpassword-{environment}";
        }
        
        public static int GetDatabasePort(string environment)
        {
            return 5432;
        }
    }
}
