using System;
using Services.User.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class GetUserService : ServiceStack.Service
    {
        public GetUserResponse Get(GetUser request)
        {
            using (var transaction = Db.OpenTransaction())
            {
                var data = Db.SingleById<UserData>(request.Id);
                if (data == null)
                {
                    throw HttpError.NotFound("User does not exist");
                }
                
                var User = data.ConvertTo<UserModel>();
                return new GetUserResponse { User = User };
            }
        }
    }
}