using System;
using Services.Membership.Models;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Services.Membership
{
    public class CreateMemberService : ServiceStack.Service
    {
        public CreateMemberResponse Post(CreateMember request)
        {
            bool exists = Db.Exists<MemberData>(x => x.AccountId == request.AccountId && x.UserId == request.UserId);

            if (!exists)
            {
                Db.Insert(new MemberData { Id= Guid.NewGuid(), UserId = request.UserId, AccountId = request.AccountId });
            }

            return new CreateMemberResponse { };
        }
    }
}