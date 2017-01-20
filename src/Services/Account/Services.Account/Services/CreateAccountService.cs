using System;
using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class CreateAccountService : IService
    {
        IDbConnectionFactory _dbFactory;

        public CreateAccountService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public CreateAccountResponse Post(CreateAccount request)
        {
            var id = request.Id;
            var name = request.Name;

            Validate(id);
            Validate(name);

            if (!Exists(id))
            {
                Create(id, name);
            }

            var data = Get(id);
            var model = data.ConvertTo<AccountModel>();

            return new CreateAccountResponse { Account = model };
        }

        AccountData Get(Guid id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<AccountData>(id);
            }
        }

        void Create(Guid id, string name)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(new AccountData { Id = id, Name = name });
            }
        }

        bool Exists(Guid id)
        {
            return Get(id) != null;
        }

        void Validate(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be supplied");
            }
        }

        void Validate(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be supplied");
            }
        }
    }
}