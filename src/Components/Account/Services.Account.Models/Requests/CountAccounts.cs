using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account/Count", "GET", Summary = "Count all Accounts")]
    public class CountAccounts : IGet, IReturn<CountAccountsResponse>
    {

    }

    public class CountAccountsResponse
    {
        public long Total { get; set; }
    }
}