
using System;
using ServiceStack;

namespace Services.Gateway
{
    public class BaseService : Service
    {
        public Guid CurrentAccountId
        {
            get
            {
                return SessionAs<CustomAuthUserSession>().AccountId;
            }
        }
        public Guid CurrentUserId
        {
            get
            {
                return SessionAs<CustomAuthUserSession>().UserId;
            }
        }
    }
}