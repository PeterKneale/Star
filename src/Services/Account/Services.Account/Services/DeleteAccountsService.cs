using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class DeleteAccountsService : IService
    {
        IDbConnectionFactory _dbFactory;

        public DeleteAccountsService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public DeleteAccountsResponse Delete(DeleteAccounts request)
        {
            Delete();

            return new DeleteAccountsResponse();
        }

        void Delete()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteAll<AccountData>();
            }
        }
    }
}