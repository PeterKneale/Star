using System;
using Services.User.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class UpdateUserService : IService
    {
        IDbConnectionFactory _dbFactory;

        public UpdateUserService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public UpdateUserResponse Put(UpdateUser request)
        {
            var id = request.Id;
            var firstName = request.FirstName;
            var lastName = request.LastName;

            Validate(id);
            Validate(firstName);
            Validate(lastName);

            if (!Exists(id))
            {
                throw HttpError.NotFound("User does not exist");
            }
            
            Update(id, firstName, lastName);

            var data = Get(id);
            var model = data.ConvertTo<UserModel>();

            return new UpdateUserResponse { User = model };
        }

        UserData Get(Guid id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<UserData>(id);
            }
        }

        void Update(Guid id, string firstName, string lastName)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Update(new UserData { Id = id, FirstName = firstName, LastName = lastName });
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