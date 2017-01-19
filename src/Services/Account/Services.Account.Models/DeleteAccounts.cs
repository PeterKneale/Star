using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account", "DELETE", Summary = "Delete Accounts")]
    public class DeleteAccounts : IReturn<DeleteAccountResponse>
    {
        
    }

    public class DeleteAccountsResponse
    {
        public string Result { get; set; }
    }

    public class AccountsDeletedEvent
    {
    }
}