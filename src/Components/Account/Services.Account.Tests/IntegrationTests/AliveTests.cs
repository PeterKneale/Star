using System;
using Services.Account.Models;
using Xunit;

namespace Services.Account.Tests
{
    public class AliveTests : IDisposable
    {
        private TestContext _context;

        public AliveTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public void Alive()
        {
            // arrange
            var service = _context.Resolve<AliveService>();

            // act
            var response = service.Get(new Alive());

            // assert
            Assert.NotNull(response);
            Assert.Equal("Account", response.ServiceName);
        }
    }
}