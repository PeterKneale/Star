using Services.User.Models;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;
using ServiceStack.Data;

namespace Services.User
{
    public class GetUsersService : IService
    {
        IDbConnectionFactory _dbFactory;

        public GetUsersService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public GetUsersResponse Get(GetUsers request)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var data = db.Select<UserData>();

                var Users = data.Select(x => x.ConvertTo<UserModel>()).ToArray();
                return new GetUsersResponse { Users = Users };
            }
        }

    }
}