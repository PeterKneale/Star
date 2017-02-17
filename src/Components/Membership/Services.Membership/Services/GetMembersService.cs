using Services.Membership.Models;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;
using ServiceStack.Data;

namespace Services.Membership
{
    public class GetMembersService : IService
    {
        IDbConnectionFactory _dbFactory;

        public GetMembersService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public GetMembersResponse Get(GetMembers request)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var userIds = db.Select<MemberData>()
                .Where(x => x.AccountId == request.AccountId)
                .Select(x => x.UserId).ToArray();
                return new GetMembersResponse { UserIds = userIds };
            }

        }

    }
}