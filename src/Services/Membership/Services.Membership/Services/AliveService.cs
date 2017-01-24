using Services.Membership.Models;
using ServiceStack;

namespace Services.Membership
{
    public class AliveService : IService
    {
        public AliveResponse Get(Alive request)
        {
            return new AliveResponse { ServiceName ="Membership" };
        }
    }
}