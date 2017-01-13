using Services.User.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class CreateUserService : ServiceStack.Service
    {
        public CreateUserResponse Post(CreateUser request)
        {
            Db.Insert(new UserData { Id = request.Id, FirstName = request.FirstName, LastName = request.LastName });
       
            var model = Db.SingleById<UserData>(request.Id).ConvertTo<UserModel>();
            return new CreateUserResponse { User = model };
        }
    }
}