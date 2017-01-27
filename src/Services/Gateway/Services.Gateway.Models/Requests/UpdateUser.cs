using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    [Route("/Users/{id}", "PUT", Summary = "Update User")]
    public class UpdateUser : IPut, IReturn<UpdateUserResponse>
    {
        [ApiMember(Name = "Id", Description = "Identifier", ParameterType = "path", DataType = "guid", IsRequired = true)]
        public Guid Id { get; set; }
        
        [ApiMember(Name = "FirstName", Description = "First Name", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string FirstName { get; set; }

        [ApiMember(Name = "LastName", Description = "Last Name", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string LastName { get; set; }
    }

    public class UpdateUserResponse
    {
        public UserModel User { get; set; }
    }
}
