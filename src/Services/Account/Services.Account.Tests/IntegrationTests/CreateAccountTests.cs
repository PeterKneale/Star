using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class CreateAccountTests 
    {
        private TestContext _context;

        public CreateAccountTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void AccountCanBeCreated()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var service = _context.Resolve<CreateAccountService>();

            // act
            var response = service.Post(new CreateAccount { Id = id, Name = name });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.Account);
            Assert.Equal(id, response.Account.Id);
            Assert.Equal(name, response.Account.Name);
        }

        [Fact]
        public void AccountCreationIdempotent()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var service = _context.Resolve<CreateAccountService>();

            // act
            var response1 = service.Post(new CreateAccount { Id = id, Name = name });
            var response2 = service.Post(new CreateAccount { Id = id, Name = name }); // Second create call with the same id

            // assert
            Assert.NotNull(response1);
            Assert.NotNull(response1.Account);
            Assert.Equal(id, response1.Account.Id);
            Assert.Equal(name, response1.Account.Name);
            
            Assert.NotNull(response2);
            Assert.NotNull(response2.Account);
            Assert.Equal(id, response2.Account.Id);
            Assert.Equal(name, response2.Account.Name);
        }

        [Fact]
        public void AccountIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var name = "Account Name";
            var service = _context.Resolve<CreateAccountService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Post(new CreateAccount { Id = id, Name = name }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }

        [Fact]
        public void AccountNameIsMandatory()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd63");
            var name = string.Empty;
            var service = _context.Resolve<CreateAccountService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Post(new CreateAccount { Id = id, Name = name }));

            // assert
            Assert.Equal("Name must be supplied", ex.Message);
        }
    }
}