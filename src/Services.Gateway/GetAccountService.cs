using ServiceStack;
using Services.Gateway.Models;
using Accounts = Services.Account.Models;
using System.Threading.Tasks;
using System;


namespace Services.Gateway
{
    public class GetAccountService : Service
    {
        public async Task<GetAccountResponse> Post(GetAccount request)
        {
            var accountRequest = new Accounts.GetAccount { Id = Guid.NewGuid() };

            var accountResponse = await Gateway.SendAsync(accountRequest);
 
            return new GetAccountResponse
            {
                Account = accountResponse.Account.ConvertTo<AccountModel>()
            };
        }
    }
}