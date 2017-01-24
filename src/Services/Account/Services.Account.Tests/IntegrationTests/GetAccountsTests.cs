using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class GetAccountsTests 
    {
        private TestContext _context;

        public GetAccountsTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void AccountsCanBeRetrieved()
        {
            // arrange
            var accounts = 10;
            var name = "Account Name";
            var service = _context.Resolve<GetAccountsService>();
            var createAccountService = _context.Resolve<CreateAccountService>();

            // act
            for (var x = 1; x <= accounts; x++)
            {
                createAccountService.Post(new CreateAccount { Id = Guid.NewGuid(), Name = name });
            }
            var response = service.Get(new GetAccounts ());

            // assert
            Assert.NotNull(response);
            Assert.NotNull(response.Accounts);
            Assert.Equal(accounts, response.Accounts.Length);
        }

    }
}