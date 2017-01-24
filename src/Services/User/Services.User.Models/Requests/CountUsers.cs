using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/User/Count", "GET", Summary = "Count all Users")]
    public class CountUsers : IGet, IReturn<CountUsersResponse>
    {

    }

    public class CountUsersResponse
    {
        public long Total { get; set; }
    }
}