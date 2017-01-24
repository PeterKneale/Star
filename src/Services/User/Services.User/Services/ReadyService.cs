using Services.User.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class ReadyService : IService
    {
        IDbConnectionFactory _dbFactory;

        public ReadyService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public ReadyResponse Get(Ready request)
        {
            CheckDatabaseConnection();
            return new ReadyResponse { ServiceName ="User" };
        }

        void CheckDatabaseConnection()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.SqlScalar<int>("Select 1");
            }
        }
    }
}