using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account", "PUT", Summary = "Update an Account")]
    public class UpdateAccount : IPut, IReturn<UpdateAccountResponse>
    {
        [ApiMember(Name = "Id", Description = "Id", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid Id { get; set; }

        [ApiMember(Name = "Name", Description = "Name", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string Name { get; set; }
    }

    public class UpdateAccountResponse
    {
        public AccountModel Account { get; set; }
    }

    public class AccountUpdatedEvent
    {
        public Guid Id { get; set; }
    }
}
