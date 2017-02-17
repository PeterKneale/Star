using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class CountAccountsService : IService
    {
        IDbConnectionFactory _dbFactory;

        public CountAccountsService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public CountAccountsResponse Get(CountAccounts request)
        {
            var total = CountAccounts();

            return new CountAccountsResponse { Total = total };
        }

        long CountAccounts()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Count<AccountData>();
            }
        }
    }
}