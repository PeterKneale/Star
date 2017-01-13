using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account", "GET", Summary = "Get all Accounts")]
    public class GetAccounts : IReturn<GetAccountsResponse>
    {

    }

    public class GetAccountsResponse
    {
        public AccountModel[] Accounts { get; set; }
        public long Total { get; set; }
        public string Result { get; set; }
    }
}