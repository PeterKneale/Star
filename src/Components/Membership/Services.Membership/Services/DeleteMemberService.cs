using System;
using Services.Membership.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Membership
{
    public class DeleteMemberService : IService
    {
        IDbConnectionFactory _dbFactory;

        public DeleteMemberService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public DeleteMemberResponse Delete(DeleteMember request)
        {
            var accountId = request.AccountId;
            var userId = request.UserId;

            Validate(accountId);
            Validate(userId);

           
            return new DeleteMemberResponse();
        }        

        void Validate(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be supplied");
            }
        }

        void Delete(Guid accountId, Guid userId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                 db.Delete<MemberData>(x => x.AccountId == accountId && x.UserId == userId);
            }
        }
    }
}