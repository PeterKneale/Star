using System;
using ServiceStack;

namespace Services.Membership.Models
{
    [Route("/Members", "POST", Summary = "Create an account membership")]
    public class CreateMember : IReturn<CreateMemberResponse>
    {
        [ApiMember(Name = "UserId", Description = "UserId", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid UserId { get; set; }
        [ApiMember(Name = "AccountId", Description = "UserId", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid AccountId { get; set; }
    }

    public class CreateMemberResponse
    {
        
    }

    public class MemberCreatedEvent
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
    }
}
