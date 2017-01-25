using System;
using ServiceStack.DataAnnotations;

namespace Services.Membership
{
    [Alias("Members")]
    public class MemberData
    {
        [AutoIncrement]
        public long Id { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
