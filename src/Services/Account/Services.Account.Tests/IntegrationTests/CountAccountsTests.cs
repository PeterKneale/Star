using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class CountAccountsTests : IDisposable
    {
        private TestContext _context;

        public CountAccountsTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public void NoAccountsWhenEmpty()
        {
            // arrange
            var service = _context.Resolve<CountAccountsService>();

            // act
            var response = service.Get(new CountAccounts());

            // assert
            Assert.NotNull(response);
            Assert.Equal(0, response.Total);
        }

        [Fact]
        public void CountIncrements()
        {
            // arrange
            var accounts = 10;
            var name = "Account Name";
            var createAccountService = _context.Resolve<CreateAccountService>();
            var countAccountService = _context.Resolve<CountAccountsService>();

            // act
            for (var x = 1; x <= accounts; x++)
            {
                createAccountService.Post(new CreateAccount { Id = Guid.NewGuid(), Name = name });
            }
            var countResponse = countAccountService.Get(new CountAccounts());

            // assert
            Assert.NotNull(countResponse);
            Assert.Equal(accounts, countResponse.Total);
        }

        [Fact]
        public void CountDecrementsOnDeletion()
        {
            // arrange
            var id = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var name = "Account Name";
            var createAccountService = _context.Resolve<CreateAccountService>();
            var countAccountService = _context.Resolve<CountAccountsService>();
            var deleteAccountService = _context.Resolve<DeleteAccountService>();

            // act
            var countResponsePre = countAccountService.Get(new CountAccounts());

            createAccountService.Post(new CreateAccount { Id = id, Name = name });

            var countResponsePostCreate = countAccountService.Get(new CountAccounts());

            deleteAccountService.Delete(new DeleteAccount { Id = id });

            var countResponsePostDelete = countAccountService.Get(new CountAccounts());

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