using System;
using ServiceStack.DataAnnotations;

namespace Services.Account
{
    public class AccountData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
