using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/User", "POST", Summary = "Create a User")]
    public class CreateUser : IReturn<CreateUserResponse>
    {
        [ApiMember(Name = "Id", Description = "Id", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid Id { get; set; }

        [ApiMember(Name = "FirstName", Description = "FirstName", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }
        
        [ApiMember(Name = "LastName", Description = "LastName", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }
    }

    public class CreateUserResponse
    {
        public UserModel User { get; set; }
        public string Result { get; set; }
    }

    public class UserCreatedEvent
    {
        public Guid Id { get; set; }
    }
}
