using Services.Membership.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Membership
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
            return new ReadyResponse { ServiceName ="Membership" };
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