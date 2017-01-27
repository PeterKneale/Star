using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    [Route("/Users/{id}", "GET", Summary = "Get User")]
    public class GetUser : IGet, IReturn<GetUserResponse>
    {
        [ApiMember(Name = "Id", Description = "Identifier", ParameterType = "path", DataType = "guid", IsRequired = true)]
        public Guid Id { get; set; }
    }

    public class GetUserResponse
    {
        public UserModel User { get; set; }
    }
}
