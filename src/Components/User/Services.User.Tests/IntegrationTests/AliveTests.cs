using System;
using Services.User.Models;
using Xunit;

namespace Services.User.Tests
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
            Assert.Equal("User", response.ServiceName);
        }
    }
}