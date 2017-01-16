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
            
            var listUsersRequest = new Members.GetMembers{ AccountId = accountId };
            var listUsersResponse = await Gateway.SendAsync(listUsersRequest);
            
            var userIds = listUsersResponse.Users;

            var getUsersRequest = new Users.GetUsers { UserIds = userIds };
            Console.WriteLine(getUsersRequest.ToJson());
            var getUsersResponse = await Gateway.SendAsync(getUsersRequest);

            var users = getUsersResponse.Users.Select(x=>x.ConvertTo<UserModel>()).ToArray();
            var total = listUsersResponse.Total;

            return new GetUsersResponse
            {
                Users = users,
                Total = total
            };
        }
    }
}