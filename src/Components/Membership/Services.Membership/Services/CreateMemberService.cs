using System;
using Services.Membership.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Membership
{
    public class CreateMemberService : IService
    {
        IDbConnectionFactory _dbFactory;

        public CreateMemberService(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public CreateMemberResponse Post(CreateMember request)
        {
            var accountId = request.AccountId;
            var userId = request.UserId;

            Validate(accountId);
            Validate(userId);

            if (!Exists(accountId, userId))
            {
                Create(accountId, userId);
            }

            return new CreateMemberResponse { AccountId = accountId, UserId = userId };
        }

        void Create(Guid accountId, Guid userId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(new MemberData { AccountId = accountId, UserId = userId });
            }
        }
        
        bool Exists(Guid accountId, Guid userId)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<MemberData>(x => x.AccountId == accountId && x.UserId == userId);
            }
        }

        void Validate(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be supplied");
            }
        }
    }
}