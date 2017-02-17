using ServiceStack;
using Services.Gateway.Models;
using Users = Services.User.Models;
using System.Threading.Tasks;
using System;

namespace Services.Gateway
{

    [Authenticate()]
    public class GetUserService : BaseService
    {
        public async Task<GetUserResponse> Get(GetUser request)
        {
            var getRequest = new Users.GetUser { Id = request.Id };

            var getResponse = await Gateway.SendAsync(getRequest);

            var model = getResponse.User.ConvertTo<UserModel>();

            return new GetUserResponse { User = model };
        }
    }
}