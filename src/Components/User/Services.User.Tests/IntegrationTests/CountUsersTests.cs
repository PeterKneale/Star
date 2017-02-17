using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
{
    public class CountUsersTests : IDisposable
    {
        private TestContext _context;

        public CountUsersTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public void NoUsersWhenEmpty()
        {
            // arrange
            var service = _context.Resolve<CountUsersService>();

            // act
            var response = service.Get(new CountUsers());

            // assert
            Assert.NotNull(response);
            Assert.Equal(0, response.Total);
        }

        [Fact]
        public void CountIncrements()
        {
            // arrange
            var Users = 10;
            var first = "first";
            var last = "last";
            var createUserService = _context.Resolve<CreateUserService>();
            var countUserService = _context.Resolve<CountUsersService>();

            // act
            for (var x = 1; x <= Users; x++)
            {
                createUserService.Post(new CreateUser { Id = Guid.NewGuid(), FirstName = first, LastName = last });
            }
            var countResponse = countUserService.Get(new CountUsers());

            // assert
            Assert.NotNull(countResponse);
            Assert.Equal(Users, countResponse.Total);
        }

        [Fact]
        public void CountDecrementsOnDeletion()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var createUserService = _context.Resolve<CreateUserService>();
            var countUserService = _context.Resolve<CountUsersService>();
            var deleteUserService = _context.Resolve<DeleteUserService>();

            // act
            var countResponsePre = countUserService.Get(new CountUsers());

            createUserService.Post(new CreateUser { Id = id, FirstName = first, LastName = last });

            var countResponsePostCreate = countUserService.Get(new CountUsers());

            deleteUserService.Delete(new DeleteUser { Id = id });

            var countResponsePostDelete = countUserService.Get(new CountUsers());

            // assert
            Assert.NotNull(countResponsePre);
            Assert.NotNull(countResponsePostCreate);
            Assert.NotNull(countResponsePostDelete);
            Assert.Equal(0, countResponsePre.Total);
            Assert.Equal(1, countResponsePostCreate.Total);
            Assert.Equal(0, countResponsePostDelete.Total);
        }
    }
}