using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
{
    public class UpdateUserTests 
    {
        private TestContext _context;

        public UpdateUserTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void UserCanBeUpdated()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var firstUpdated = "firstUpdated";
            var lastUpdated = "lastUpdated";

            var createService = _context.Resolve<CreateUserService>();
            var updateService = _context.Resolve<UpdateUserService>();
            var getService = _context.Resolve<GetUserService>();

            // act
            createService.Post(new CreateUser { Id = id, FirstName = first, LastName = last });
            updateService.Put(new UpdateUser { Id = id, FirstName = firstUpdated, LastName = lastUpdated });
            var response = getService.Get(new GetUser { Id = id });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.User);
            Assert.Equal(id, response.User.Id);
            Assert.Equal(firstUpdated, response.User.FirstName);
            Assert.Equal(lastUpdated, response.User.LastName);
        }

        [Fact]
        public void UserIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var first = "first";
            var last = "last";
            var service = _context.Resolve<UpdateUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Put(new UpdateUser { Id = id,FirstName = first, LastName = last }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }

        [Fact]
        public void FirstNameIsMandatory()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd63");
            var first = string.Empty;
            var last = "last";
            var service = _context.Resolve<UpdateUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Put(new UpdateUser { Id = id,FirstName = first, LastName = last }));

            // assert
            Assert.Equal("Name must be supplied", ex.Message);
        }

        [Fact]
        public void LastNameIsMandatory()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd63");
            var first = "first";
            var last = string.Empty;
            var service = _context.Resolve<UpdateUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Put(new UpdateUser { Id = id,FirstName = first, LastName = last }));

            // assert
            Assert.Equal("Name must be supplied", ex.Message);
        }
        
        [Fact]
        public void OtherUsersAreNotUpdated()
        {
            // arrange
            var id1 = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var id2 = new Guid("c2347dce11114128be1d3537b02dcd63");
            var first = "first";
            var last = "last";
            var firstUpdated = "firstUpdated";
            var lastUpdated = "lastUpdated";

            var createService = _context.Resolve<CreateUserService>();
            var updateService = _context.Resolve<UpdateUserService>();
            var getService = _context.Resolve<GetUserService>();

            // act
            createService.Post(new CreateUser { Id = id1, FirstName = first, LastName = last });
            createService.Post(new CreateUser { Id = id2, FirstName = first, LastName = last });
            updateService.Put(new UpdateUser { Id = id1, FirstName = firstUpdated, LastName = lastUpdated }); // Only update #1
            var response = getService.Get(new GetUser { Id = id2 });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.User);
            Assert.Equal(id2, response.User.Id);
            Assert.Equal(first, response.User.FirstName);   // #2 should be original
            Assert.Equal(last, response.User.LastName);     // #2 should be original
        }

    }
}