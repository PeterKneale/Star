using System;
using Services.Account.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class GetAccountService : ServiceStack.Service
    {
        public GetAccountResponse Get(GetAccount request)
        {
            using (var transaction = Db.OpenTransaction())
            {
                var data = Db.SingleById<AccountData>(request.Id);
                if (data == null)
                {
                    throw HttpError.NotFound("Account does not exist");
                }
                
                var Account = data.ConvertTo<AccountModel>();
                return new GetAccountResponse { Account = Account };
            }
        }
    }
}