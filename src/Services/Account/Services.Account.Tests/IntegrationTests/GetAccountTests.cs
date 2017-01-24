using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class GetAccountTests 
    {
        private TestContext _context;

        public GetAccountTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void AccountCanBeRetrieved()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var service = _context.Resolve<GetAccountService>();
            var createAccountService = _context.Resolve<CreateAccountService>();

            // act
            createAccountService.Post(new CreateAccount { Id = id, Name = name });
            var response = service.Get(new GetAccount { Id = id });

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.Account);
            Assert.Equal(id, response.Account.Id);
            Assert.Equal(name, response.Account.Name);
        }


        [Fact]
        public void AccountIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var service = _context.Resolve<GetAccountService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Get(new GetAccount { Id = id }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }

        [Fact]
        public void AccountNotFound()
        {
            // arrange
            var id = new Guid("aaa47dce23074128be1d3537b02dcd63");
            var service = _context.Resolve<GetAccountService>();

            // act
            Exception ex = Assert.Throws<ServiceStack.HttpError>(() => service.Get(new GetAccount { Id = id }));

            // assert
            Assert.Equal("Account does not exist", ex.Message);
        }
        
        [Fact]
        public void AccountCannotBeRetrievedAfterDeletion()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var getService = _context.Resolve<GetAccountService>();
            var createService = _context.Resolve<CreateAccountService>();
            var deleteService = _context.Resolve<DeleteAccountService>();

            // act
            createService.Post(new CreateAccount { Id = id, Name = name });
            getService.Get(new GetAccount { Id = id });
            deleteService.Delete(new DeleteAccount { Id = id });

            Exception ex = Assert.Throws<ServiceStack.HttpError>(() => getService.Get(new GetAccount { Id = id }));

            // assert
            Assert.Equal("Account does not exist", ex.Message);
        }

    }
}
