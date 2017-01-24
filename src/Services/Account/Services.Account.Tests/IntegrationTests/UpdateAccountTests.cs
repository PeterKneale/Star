using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class UpdateAccountTests 
    {
        private TestContext _context;

        public UpdateAccountTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void AccountCanBeUpdated()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var nameUpdated = "Account Name Updated";

            var createService = _context.Resolve<CreateAccountService>();
            var updateService = _context.Resolve<UpdateAccountService>();
            var getService = _context.Resolve<GetAccountService>();

            // act
            createService.Post(new CreateAccount { Id = id, Name = name });
            updateService.Put(new UpdateAccount { Id = id, Name = nameUpdated });
            var response = getService.Get(new GetAccount { Id = id });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.Account);
            Assert.Equal(id, response.Account.Id);
            Assert.Equal(nameUpdated, response.Account.Name);
        }

        [Fact]
        public void AccountIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var name = "Account Name";
            var service = _context.Resolve<UpdateAccountService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Put(new UpdateAccount { Id = id, Name = name }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }

        [Fact]
        public void AccountNameIsMandatory()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd63");
            var name = string.Empty;
            var service = _context.Resolve<UpdateAccountService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Put(new UpdateAccount { Id = id, Name = name }));

            // assert
            Assert.Equal("Name must be supplied", ex.Message);
        }
        
        [Fact]
        public void OtherAccountsAreNotUpdated()
        {
            // arrange
            var id1 = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var id2 = new Guid("c2347dce11114128be1d3537b02dcd63");
            var name = "Account Name";
            var nameUpdated = "Account Name Updated";

            var createService = _context.Resolve<CreateAccountService>();
            var updateService = _context.Resolve<UpdateAccountService>();
            var getService = _context.Resolve<GetAccountService>();

            // act
            createService.Post(new CreateAccount { Id = id1, Name = name });
            createService.Post(new CreateAccount { Id = id2, Name = name });
            updateService.Put(new UpdateAccount { Id = id1, Name = nameUpdated }); // Only update #1
            var response = getService.Get(new GetAccount { Id = id2 });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.Account);
            Assert.Equal(id2, response.Account.Id);
            Assert.Equal(name, response.Account.Name);   // #2 should be original
        }
    }
}