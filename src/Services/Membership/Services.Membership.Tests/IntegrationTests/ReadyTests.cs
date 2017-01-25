using System;
using Services.Membership.Models;
using Xunit;

namespace Services.Membership.Tests
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
            Assert.Equal("Membership", response.ServiceName);
        }
    }
}