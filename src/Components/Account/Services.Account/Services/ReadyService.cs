using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account
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
            return new ReadyResponse { ServiceName ="Account" };
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