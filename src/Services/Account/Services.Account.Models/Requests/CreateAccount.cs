using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account", "POST", Summary = "Create an Account")]
    public class CreateAccount : IReturn<CreateAccountResponse>
    {
        [ApiMember(Name = "Id", Description = "Id", ParameterType = "path", DataType = "uniqueidentifier", IsRequired = true)]
        public Guid Id { get; set; }

        [ApiMember(Name = "Name", Description = "Name", ParameterType = "path", DataType = "string", IsRequired = true)]
        public string Name { get; set; }
    }

    public class CreateAccountResponse
    {
        public AccountModel Account { get; set; }
    }

    public class AccountCreatedEvent
    {
        public Guid Id { get; set; }
    }
}
