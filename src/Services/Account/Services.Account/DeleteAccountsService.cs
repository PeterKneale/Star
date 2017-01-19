using Services.Account.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class DeleteAccountsService : Service
    {
        public DeleteAccountsResponse Delete(DeleteAccounts request)
        {
            Db.DeleteAll<AccountData>();

            return new DeleteAccountsResponse();
        }
    }
}