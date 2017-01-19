using System;
using ServiceStack;

namespace Services.Gateway.Models
{
    [Route("/account", "GET", Summary = "Get  Account")]
    public class GetAccount : IReturn<CreateAccountResponse>
    {
        
    }

    public class GetAccountResponse
    {
        public AccountModel Account { get; set; }
    }
}
