using Services.User.Models;
using ServiceStack;

namespace Services.User
{
    public class AliveService : IService
    {
        public AliveResponse Get(Alive request)
        {
            return new AliveResponse { ServiceName ="User" };
        }
    }
}