using System;
using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class GetAccountService : IService
    {
        IDbConnectionFactory _dbFactory;

        public GetAccountService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public GetAccountResponse Get(GetAccount request)
        {
            var id = request.Id;

            Validate(id);

            if (!Exists(id))
            {
                throw HttpError.NotFound("Account does not exist");
            }

            var data = Get(id);

            var model = data.ConvertTo<AccountModel>();
            return new GetAccountResponse { Account = model };
        }

        void Validate(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be supplied");
            }
        }

        bool Exists(Guid id)
        {
            return Get(id) != null;
        }

        AccountData Get(Guid id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<AccountData>(id);
            }
        }
    }
}