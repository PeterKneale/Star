using Services.Membership.Models;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;

namespace Services.Membership
{
    public class GetMembersService : Service
    {
        public GetMembersResponse Get(GetMembers request)
        {
            var userIds = Db.Select<MemberData>()
                .Where(x => x.AccountId == request.AccountId)
                .Select(x => x.UserId).ToArray();
            return new GetMembersResponse { UserIds = userIds };
        }

    }
}