using Services.Account.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class DeleteAccountService : Service
    {
        public DeleteAccountResponse Delete(DeleteAccount request)
        {
            Db.DeleteById<AccountData>(request.Id);

            return new DeleteAccountResponse();
        }
    }
}