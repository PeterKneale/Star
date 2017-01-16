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
            using (var transaction = Db.OpenTransaction())
            {
                var data = Db.Select<MemberData>().Where(x => x.AccountId == request.AccountId);
                var count = Db.Count<MemberData>(x => x.AccountId == request.AccountId);

                var Users = data.Select(x => x.UserId).ToArray();
                return new GetMembersResponse { Users = Users, Total = count };
            }
        }

    }
}