using System;
using Services.Account.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class CreateAccountService : ServiceStack.Service
    {
        public CreateAccountResponse Post(CreateAccount request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentNullException("Id must be supplied");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException("Name must be supplied");
            }

            Db.Insert(new AccountData { Id = request.Id, Name = request.Name });

            var model = Db.SingleById<AccountData>(request.Id).ConvertTo<AccountModel>();

            return new CreateAccountResponse { Account = model };
        }
    }
}