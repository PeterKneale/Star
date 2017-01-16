using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/User/{id}", "GET", Summary = "Get an User")]
    public class GetUser : IGet,IReturn<GetUserResponse>
    {
        [ApiMember(Name="Id", Description = "Identifier", ParameterType = "path", DataType = "guid", IsRequired = true)]
        public Guid Id { get; set; }
    }

    public class GetUserResponse
    {
        public UserModel User { get; set; }
        public string Result { get; set; }
    }
}