using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    
    [Route("/Account", "GET", Summary = "Get  Account")]
    public class GetAccount : IGet, IReturn<CreateAccountResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetAccountResponse
    {
        public AccountModel Account { get; set; }
    }
}
