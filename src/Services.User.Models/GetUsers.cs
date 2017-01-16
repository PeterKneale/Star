using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/User", "GET", Summary = "Get all Users")]
    public class GetUsers : IGet,IReturn<GetUsersResponse>
    {
        public Guid[] UserIds { get; set; }
    }

    public class GetUsersResponse
    {
        public UserModel[] Users { get; set; }
    }
}