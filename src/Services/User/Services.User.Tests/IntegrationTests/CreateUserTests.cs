using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
{
    public class CreateUserTests 
    {
        private TestContext _context;

        public CreateUserTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void UserCanBeCreated()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first = "first";
            var last = "last";
            var service = _context.Resolve<CreateUserService>();

            // act
            var response = service.Post(new CreateUser { Id = id, FirstName = first, LastName = last });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.User);
            Assert.Equal(id, response.User.Id);
            Assert.Equal(first, response.User.FirstName);
            Assert.Equal(last, response.User.LastName);
        }

        [Fact]
        public void UserCreationIdempotent()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var first1 = "first1";
            var last1 = "last1";
            var first2 = "first2";
            var last2 = "last2";
            var service = _context.Resolve<CreateUserService>();
            var countService = _context.Resolve<CountUsersService>();

            // act
            var createResponse1 = service.Post(new CreateUser { Id = id, FirstName = first1, LastName = last1 });
            var createResponse2 = service.Post(new CreateUser { Id = id, FirstName = first2, LastName = last2 }); // Second create call with the same id
            var countResponse = countService.Get(new CountUsers());

            // assert
            Assert.NotNull(createResponse1);
            Assert.NotNull(createResponse1.User);
            Assert.Equal(first1, createResponse1.User.FirstName);
            Assert.Equal(last1, createResponse1.User.LastName);
            
            Assert.NotNull(createResponse2);
            Assert.NotNull(createResponse2.User);
            Assert.Equal(id, createResponse2.User.Id);
            Assert.Equal(first1, createResponse2.User.FirstName);// Idempotent
            Assert.Equal(last1, createResponse2.User.LastName);// Idempotent
            
            Assert.NotNull(countResponse);
            Assert.Equal(1, countResponse.Total); // Idempotent
        }

        [Fact]
        public void UserIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var first = "first";
            var last = "last";
            var service = _context.Resolve<CreateUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Post(new CreateUser { Id = id, FirstName = first, LastName = last }));

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
            var service = _context.Resolve<CreateUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Post(new CreateUser { Id = id, FirstName = first, LastName = last }));

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
            var service = _context.Resolve<CreateUserService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Post(new CreateUser { Id = id, FirstName = first, LastName = last }));

            // assert
            Assert.Equal("Name must be supplied", ex.Message);
        }
    }
}