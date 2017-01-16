using ServiceStack;
using Services.Gateway.Models;
using Users = Services.User.Models;
using System.Threading.Tasks;
using System;

namespace Services.Gateway
{
    public class GetUserService : Service
    {
        public async Task<GetUserResponse> Get(GetUser request)
        {
            var UserRequest = new Users.GetUser { Id = Guid.NewGuid() };

            var UserResponse = await Gateway.SendAsync(UserRequest);
 
            return new GetUserResponse
            {
                User = UserResponse.User.ConvertTo<UserModel>()
            };
        }
    }
}