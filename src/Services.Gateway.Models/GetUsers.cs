using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    [Route("/Users", "GET", Summary = "Get  Users")]
    public class GetUsers : IReturn<GetUsersResponse>
    {

    }

    public class GetUsersResponse
    {
        public UserModel[] Users { get; set; }
        public long Total { get; set; }
    }
}
