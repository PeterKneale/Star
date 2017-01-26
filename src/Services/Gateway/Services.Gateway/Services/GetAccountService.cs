using ServiceStack;
using Services.Gateway.Models;
using Accounts = Services.Account.Models;
using System.Threading.Tasks;
using System;

namespace Services.Gateway
{
    [Authenticate()]
    public class GetAccountService : Service
    {
        public async Task<GetAccountResponse> Get(GetAccount request)
        {
            var accountId = Guid.Parse(GetSession().Email);

            var accountRequest = new Accounts.GetAccount { Id = accountId };

            var accountResponse = await Gateway.SendAsync(accountRequest);

            return new GetAccountResponse
            {
                Account = accountResponse.Account.ConvertTo<AccountModel>()
            };
        }
    }
}