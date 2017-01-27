using ServiceStack;
using Services.Gateway.Models;
using System.Threading.Tasks;
using System;
using Members = Services.Membership.Models;
using Users = Services.User.Models;
using System.Linq;

namespace Services.Gateway
{
    [Authenticate()]    
    public class GetUsersService : BaseService
    {
        public async Task<GetUsersResponse> Get(GetUsers request)
        {
            var members = await Gateway.SendAsync(new Members.GetMembers{ AccountId = CurrentAccountId });
            
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