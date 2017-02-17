using Services.Account.Models;
using ServiceStack;

namespace Services.Account
{
    public class AliveService : IService
    {
        public AliveResponse Get(Alive request)
        {
            return new AliveResponse { ServiceName ="Account" };
        }
    }
}