using System;
using ServiceStack.DataAnnotations;

namespace Services.Membership
{
    public class MemberData
    {
        [AutoIncrement]
        public long Id { get; set; }
        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }
    }
}
