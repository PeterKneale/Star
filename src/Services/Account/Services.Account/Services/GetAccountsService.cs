using Services.Account.Models;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;
using ServiceStack.Data;

namespace Services.Account
{
    public class GetAccountsService : IService
    {
        IDbConnectionFactory _dbFactory;

        public GetAccountsService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public GetAccountsResponse Get(GetAccounts request)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var data = db.Select<AccountData>();
                var count = db.Count<AccountData>();

                var Accounts = data.Select(x => x.ConvertTo<AccountModel>()).ToArray();
                return new GetAccountsResponse { Accounts = Accounts, Total = count };
            }
        }

    }
}