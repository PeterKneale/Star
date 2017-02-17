using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/User", "PUT", Summary = "Update a User")]
    public class UpdateUser : IPut, IReturn<UpdateUserResponse>
    {
        [ApiMember(Name = "Id", Description = "Id", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid Id { get; set; }

        [ApiMember(Name = "FirstName", Description = "FirstName", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }
        
        [ApiMember(Name = "LastName", Description = "LastName", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }
    }

    public class UpdateUserResponse
    {
        public UserModel User { get; set; }
    }
}
