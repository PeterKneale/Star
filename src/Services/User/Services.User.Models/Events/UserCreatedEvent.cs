using System;

namespace Services.User.Models
{
    public class UserCreatedEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
