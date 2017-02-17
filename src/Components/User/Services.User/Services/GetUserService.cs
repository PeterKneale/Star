using System;
using Services.User.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class GetUserService : IService
    {
        IDbConnectionFactory _dbFactory;

        public GetUserService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public GetUserResponse Get(GetUser request)
        {
            var id = request.Id;

            Validate(id);

            if (!Exists(id))
            {
                throw HttpError.NotFound("User does not exist");
            }

            var data = Get(id);

            var model = data.ConvertTo<UserModel>();
            return new GetUserResponse { User = model };
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

        UserData Get(Guid id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<UserData>(id);
            }
        }
    }
}