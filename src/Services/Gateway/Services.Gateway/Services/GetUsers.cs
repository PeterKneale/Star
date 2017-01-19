using ServiceStack;
using Services.Gateway.Models;
using System.Threading.Tasks;
using System;
using Members = Services.Membership.Models;
using Users = Services.User.Models;
using System.Linq;

namespace Services.Gateway
{
    public class GetUsersService : Service
    {
        public async Task<GetUsersResponse> Get(GetUsers request)
        {
            var accountId = Guid.Parse("cd9d1dfca6194a978b2bf3b9fa9b4208"); // TODO: Fetch current users account id.
            
            var members = await Gateway.SendAsync(new Members.GetMembers{ AccountId = accountId });
            
            var userIds = members.UserIds;

            var users = await Gateway.SendAsync(new Users.GetUsers { UserIds = userIds });

            var userModels = users.Users.Select(x=>x.ConvertTo<UserModel>()).ToArray();

            return new GetUsersResponse
            {
                Users = userModels
            };
        }
    }
}