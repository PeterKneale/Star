using System;
using ServiceStack;

namespace Services.Membership.Models
{
    [Route("/Ready", "GET", Summary = "Check service is ready")]
    public class Ready : IGet, IReturn<ReadyResponse>
    {

    }

    public class ReadyResponse
    {
        public string ServiceName { get; set; }
    }
}