using Services.User.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User
{
    public class CountUsersService : IService
    {
        IDbConnectionFactory _dbFactory;

        public CountUsersService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public CountUsersResponse Get(CountUsers request)
        {
            var total = CountUsers();

            return new CountUsersResponse { Total = total };
        }

        long CountUsers()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Count<UserData>();
            }
        }
    }
}