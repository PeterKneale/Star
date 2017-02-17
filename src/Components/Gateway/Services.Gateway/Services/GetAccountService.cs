using ServiceStack;
using Services.Gateway.Models;
using Accounts = Services.Account.Models;
using System.Threading.Tasks;

namespace Services.Gateway
{
    [Authenticate()]
    public class GetAccountService : BaseService
    {
        public async Task<GetAccountResponse> Get(GetAccount request)
        {
            var accountRequest = new Accounts.GetAccount { Id = CurrentAccountId };

            var accountResponse = await Gateway.SendAsync(accountRequest);

            return new GetAccountResponse
            {
                Account = accountResponse.Account.ConvertTo<AccountModel>()
            };
        }
    }
}