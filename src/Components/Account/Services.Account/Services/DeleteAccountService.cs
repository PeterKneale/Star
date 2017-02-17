using System;
using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account
{
    public class DeleteAccountService : IService
    {
        IDbConnectionFactory _dbFactory;

        public DeleteAccountService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public DeleteAccountResponse Delete(DeleteAccount request)
        {
            var id = request.Id;

            Validate(id);

            Delete(id);

            return new DeleteAccountResponse();
        }

        void Validate(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be supplied");
            }
        }

        void Delete(Guid id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<AccountData>(id);
            }
        }
    }
}