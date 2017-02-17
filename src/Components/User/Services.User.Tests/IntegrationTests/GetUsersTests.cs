using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
{
    public class GetUsersTests 
    {
        private TestContext _context;

        public GetUsersTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void UsersCanBeRetrieved()
        {
            // arrange
            var users = 10;
            var first = "first";
            var last = "last";
            var service = _context.Resolve<GetUsersService>();
            var createUserService = _context.Resolve<CreateUserService>();

            // act
            for (var x = 1; x <= users; x++)
            {
                createUserService.Post(new CreateUser { Id = Guid.NewGuid(), FirstName = first, LastName = last });
            }
            var response = service.Get(new GetUsers ());

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.Users);
            Assert.Equal(users, response.Users.Length);
        }
    }
}