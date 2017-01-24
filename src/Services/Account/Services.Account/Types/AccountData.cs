using System;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace Services.Account
{
    [Alias("Accounts")]
    public class AccountData : IHasId<Guid>
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
