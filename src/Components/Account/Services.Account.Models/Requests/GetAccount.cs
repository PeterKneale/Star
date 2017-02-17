using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account/{id}", "GET", Summary = "Get an Account")]
    public class GetAccount : IGet, IReturn<GetAccountResponse>
    {
        [ApiMember(Name = "Id", Description = "Identifier", ParameterType = "path", DataType = "guid", IsRequired = true)]
        public Guid Id { get; set; }
    }

    public class GetAccountResponse
    {
        public AccountModel Account { get; set; }
    }
}