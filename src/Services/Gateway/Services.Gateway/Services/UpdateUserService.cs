using ServiceStack;
using Services.Gateway.Models;
using Users = Services.User.Models;
using System.Threading.Tasks;
using System;

namespace Services.Gateway
{

    [Authenticate()]
    public class UpdateUserService : BaseService
    {
        public async Task<UpdateUserResponse> Put(UpdateUser request)
        {
            var updateRequest = new Users.UpdateUser { Id = request.Id, FirstName = request.FirstName, LastName = request.LastName };
            var updateResponse = await Gateway.SendAsync(updateRequest);

            var getRequest = new Users.GetUser { Id = CurrentUserId };
            var getResponse = await Gateway.SendAsync(getRequest);
            var model = getResponse.User.ConvertTo<UserModel>();
            return new UpdateUserResponse { User = model };
        }
    }
}