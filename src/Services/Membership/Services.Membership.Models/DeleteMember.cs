using System;
using ServiceStack;

namespace Services.Membership.Models
{
    [Route("/Members", "DELETE", Summary = "Remove a user from an account")]
    public class DeleteMember : IReturn<DeleteMemberResponse>
    {
        [ApiMember(Name = "UserId", Description = "UserId", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid UserId { get; set; }
        [ApiMember(Name = "AccountId", Description = "UserId", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid AccountId { get; set; }
    }

    public class DeleteMemberResponse
    {
        
    }

    public class MemberDeletedEvent
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
    }
}
