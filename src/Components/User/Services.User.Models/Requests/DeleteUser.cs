using System;
using ServiceStack;

namespace Services.User.Models
{
    [Route("/User/{id}", "DELETE", Summary = "Delete an User")]
    public class DeleteUser : IReturn<DeleteUserResponse>
    {
        [ApiMember(Name = "Id", Description = "Identifier", ParameterType = "path", DataType = "guid", IsRequired = true)]
        public Guid Id { get; set; }
    }

    public class DeleteUserResponse
    {
        
    }

    public class UserDeletedEvent
    {
        public int Id { get; set; }
    }
}