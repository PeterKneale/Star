using System;
using ServiceStack.DataAnnotations;

namespace Services.User
{
    [Alias("Users")]
    public class UserData
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
