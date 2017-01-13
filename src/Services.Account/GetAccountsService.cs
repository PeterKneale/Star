using Services.Account.Models;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;

namespace Services.Account
{
    public class GetAccountsService : Service
    {
        public GetAccountsResponse Get(GetAccounts request)
        {
            using (var transaction = Db.OpenTransaction())
            {
                var data = Db.Select<AccountData>();
                var count = Db.Count<AccountData>();
                
                var Accounts = data.Select(x=>x.ConvertTo<AccountModel>()).ToArray();
                return new GetAccountsResponse { Accounts = Accounts, Total = count };
            }
        }

    }
}