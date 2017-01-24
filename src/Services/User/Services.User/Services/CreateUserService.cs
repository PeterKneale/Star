using System;
using Services.User.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class CreateUserService : IService
    {
        IDbConnectionFactory _dbFactory;

        public CreateUserService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public CreateUserResponse Post(CreateUser request)
        {
            var id = request.Id;
            var firstName = request.FirstName;
            var lastName = request.LastName;

            Validate(id);
            Validate(firstName);
            Validate(lastName);

            if (!Exists(id))
            {
                Create(id, firstName, lastName);
            }

            var data = Get(id);
            var model = data.ConvertTo<UserModel>();

            return new CreateUserResponse { User = model };
        }

        UserData Get(Guid id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<UserData>(id);
            }
        }
        
        void Create(Guid id, string firstName, string lastName)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(new UserData { Id = id, FirstName = firstName, LastName = lastName });
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