using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    [Route("/User", "GET", Summary = "Get  User")]
    public class GetUser : IReturn<GetUserResponse>
    {
        
    }

    public class GetUserResponse
    {
        public UserModel User { get; set; }
    }
}
