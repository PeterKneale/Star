using System;
using Services.User.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class DeleteUserService : IService
    {
        IDbConnectionFactory _dbFactory;

        public DeleteUserService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public DeleteUserResponse Delete(DeleteUser request)
        {
            var id = request.Id;

            Validate(id);

            Delete(id);

            return new DeleteUserResponse();
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
                db.DeleteById<UserData>(id);
            }
        }
    }
}