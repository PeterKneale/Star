using Services.User.Models;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;

namespace Services.User
{
    public class GetUsersService : Service
    {
        public GetUsersResponse Get(GetUsers request)
        {
            var data = Db.Select<UserData>(q => Sql.In(q.Id, request.UserIds));
            var users = data.Select(x=>x.ConvertTo<UserModel>()).ToArray();
            return new GetUsersResponse { Users = users };
        }

    }
}