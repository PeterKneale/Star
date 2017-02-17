using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
{
    public class DeleteUserTests
    {
        private TestContext _context;

        public DeleteUserTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void UserCanBeDeleted()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var createService = _context.Resolve<CreateUserService>();
            var deleteService = _context.Resolve<DeleteUserService>();
            var countService = _context.Resolve<CountUsersService>();

            // act
            createService.Post(new CreateUser { Id = id, FirstName = first, LastName = last });
            deleteService.Delete(new DeleteUser { Id = id });
            var countResponse = countService.Get(new CountUsers());

            // assert
            Assert.NotNull(countResponse);
            Assert.Equal(0, countResponse.Total);
        }

        [Fact]
        public void UserDeletionIdempotent()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var createService = _context.Resolve<CreateUserService>();
            var deleteService = _context.Resolve<DeleteUserService>();
            var countService = _context.Resolve<CountUsersService>();

            // act
            createService.Post(new CreateUser { Id = id, FirstName = first, LastName = last });
            deleteService.Delete(new DeleteUser { Id = id });
            deleteService.Delete(new DeleteUser { Id = id }); // Idempotent
            var countResponse = countService.Get(new CountUsers());

            // assert
            Assert.NotNull(countResponse);
            Assert.Equal(0, countResponse.Total);
        }

        [Fact]
        public void UserIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var service = _context.Resolve<DeleteUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Delete(new DeleteUser { Id = id }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }
    }
}