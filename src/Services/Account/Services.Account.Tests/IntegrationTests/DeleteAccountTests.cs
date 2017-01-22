using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class DeleteAccountTests
    {
        private TestContext _context;

        public DeleteAccountTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void AccountCanBeDeleted()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var createService = _context.Resolve<CreateAccountService>();
            var deleteService = _context.Resolve<DeleteAccountService>();
            var countService = _context.Resolve<CountAccountsService>();

            // act
            createService.Post(new CreateAccount { Id = id, Name = name });
            deleteService.Delete(new DeleteAccount { Id = id });
            var countResponse = countService.Get(new CountAccounts());

            // assert
            Assert.NotNull(countResponse);
            Assert.Equal(0, countResponse.Total);
        }

        [Fact]
        public void AccountDeletionIdempotent()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var createService = _context.Resolve<CreateAccountService>();
            var deleteService = _context.Resolve<DeleteAccountService>();
            var countService = _context.Resolve<CountAccountsService>();

            // act
            createService.Post(new CreateAccount { Id = id, Name = name });
            deleteService.Delete(new DeleteAccount { Id = id });
            deleteService.Delete(new DeleteAccount { Id = id }); // Idempotent
            var countResponse = countService.Get(new CountAccounts());

            // assert
            Assert.NotNull(countResponse);
            Assert.Equal(0, countResponse.Total);
        }

        [Fact]
        public void AccountIdIsMandatory()
        {
            // arrange
            var id = Guid.Empty;
            var service = _context.Resolve<DeleteAccountService>();

            // act
            Exception ex = Assert.Throws<ArgumentException>(() => service.Delete(new DeleteAccount { Id = id }));

            // assert
            Assert.Equal("Id must be supplied", ex.Message);
        }
    }
}