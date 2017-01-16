using Services.Membership.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.Membership
{
    public class DeleteMemberService : Service
    {
        public DeleteMemberResponse Delete(DeleteMember request)
        {
            Db.Delete<MemberData>(x => x.AccountId == request.AccountId && x.UserId == request.UserId);
            return new DeleteMemberResponse();
        }
    }
}