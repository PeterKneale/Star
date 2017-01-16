using ServiceStack;
using Services.Gateway.Models;
using Accounts = Services.Account.Models;
using Users = Services.User.Models;
using Members = Services.Membership.Models;
using System.Threading.Tasks;
using System;

namespace Services.Gateway
{
    public class CreateAccountService : Service
    {
        public async Task<CreateAccountResponse> Post(CreateAccount request)
        {
            var accountId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var accountRequest = new Accounts.CreateAccount
            {
                Id = accountId,
                Name = request.Name
            };
            var userRequest = new Users.CreateUser
            {
                Id = userId,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var memberRequest = new Members.CreateMember
            {
                UserId = userId,
                AccountId = accountId
            };
                
            await Gateway.SendAsync(accountRequest);
            await Gateway.SendAsync(userRequest);
            await Gateway.SendAsync(memberRequest);
 
            return new CreateAccountResponse
            {
                AccountId = accountId,
                UserId = userId
            };
        }
    }
}