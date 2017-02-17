using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
{
    public class GetUserTests 
    {
        private TestContext _context;

        public GetUserTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void UserCanBeRetrieved()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var service = _context.Resolve<GetUserService>();
            var createUserService = _context.Resolve<CreateUserService>();

            // act
            createUserService.Post(new CreateUser { Id = id, FirstName = first, LastName = last });
            var response = service.Get(new GetUser { Id = id });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.User);
            Assert.Equal(id, response.User.Id);
            Assert.Equal(first, response.User.FirstName);
            Assert.Equal(last, response.User.LastName);
        }


        [Fact]
        public void UserIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var service = _context.Resolve<GetUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Get(new GetUser { Id = id }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }

        [Fact]
        public void UserNotFound()
        {
            // arrange
            var id = new Guid("aaa47dce23074128be1d3537b02dcd63");
            var service = _context.Resolve<GetUserService>();

            // act
            Exception ex = Assert.Throws<ServiceStack.HttpError>(() => service.Get(new GetUser { Id = id }));

            // assert
            Assert.Equal("User does not exist", ex.Message);
        }
        
        [Fact]
        public void UserCannotBeRetrievedAfterDeletion()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var getService = _context.Resolve<GetUserService>();
            var createService = _context.Resolve<CreateUserService>();
            var deleteService = _context.Resolve<DeleteUserService>();

            // act
            createService.Post(new CreateUser { Id = id, FirstName = first, LastName = last });
            getService.Get(new GetUser { Id = id });
            deleteService.Delete(new DeleteUser { Id = id });

            Exception ex = Assert.Throws<ServiceStack.HttpError>(() => getService.Get(new GetUser { Id = id }));

            // assert
            Assert.Equal("User does not exist", ex.Message);
        }

    }
}