using System;

namespace Services.Membership
{
    public class MemberData
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }
    }
}
