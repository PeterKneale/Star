using ServiceStack;
using Services.Gateway.Models;
using Accounts = Services.Account.Models;
using Users = Services.User.Models;
using System.Threading.Tasks;
using System;

namespace Services.Gateway
{
    public class CreateAccountService : Service
    {
        public async Task<CreateAccountResponse> Post(CreateAccount request)
        {
            var accountRequest = new Accounts.CreateAccount
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            var userRequest = new Users.CreateUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName
            };
                
            var accountResponse = await Gateway.SendAsync(accountRequest);
            var userResponse = await Gateway.SendAsync(userRequest);

            return new CreateAccountResponse
            {
                AccountId = accountResponse.Account.Id,
                UserId = userResponse.User.Id
            };
        }
    }
}