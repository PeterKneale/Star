using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account", "GET", Summary = "Get all Accounts")]
    public class GetAccounts : IGet, IReturn<GetAccountsResponse>
    {

    }

    public class GetAccountsResponse
    {
        public AccountModel[] Accounts { get; set; }
    }
}