using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/Alive", "GET", Summary = "Check service is alive")]
    public class Alive : IGet, IReturn<AliveResponse>
    {

    }

    public class AliveResponse
    {
        public string ServiceName { get; set; }
    }
}