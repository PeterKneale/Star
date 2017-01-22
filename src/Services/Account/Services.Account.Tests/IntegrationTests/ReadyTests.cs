using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class ReadyTests : IDisposable
    {
        private TestContext _context;

        public ReadyTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public void Ready()
        {
            // arrange
            var service = _context.Resolve<ReadyService>();

            // act
            var response = service.Get(new Ready());

            // assert
            Assert.NotNull(response);
            Assert.Equal("Account", response.ServiceName);
        }
    }
}