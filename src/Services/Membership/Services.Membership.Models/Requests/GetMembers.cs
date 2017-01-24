using System;
using ServiceStack;

namespace Services.Membership.Models
{
    [Route("/Members", "GET", Summary = "List users by account")]
    public class GetMembers : IGet, IReturn<GetMembersResponse>
    {
        public Guid AccountId { get; set; }
    }

    public class GetMembersResponse
    {
        public Guid[] UserIds { get; set; }
    }
}