using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    [Route("/User", "PUT", Summary = "Update User")]
    public class UpdateUser : IPut, IReturn<UpdateUserResponse>
    {
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
