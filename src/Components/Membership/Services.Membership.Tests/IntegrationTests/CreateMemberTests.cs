using System;
using Services.Membership.Models;
using Xunit;

namespace Services.Membership.Tests
{
    public class CreateMemberTests
    {
        private TestContext _context;

        public CreateMemberTests()
        {
            _context = new TestContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void MemberCanBeCreated()
        {
            // arrange
            var accountId = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var userId = new Guid("c3147dce23074128be1d3537b02dcd22");
            var service = _context.Resolve<CreateMemberService>();

            // act
            var response = service.Post(new CreateMember { AccountId = accountId, UserId = userId });

            // assert
            Assert.NotNull(response);

            Assert.Equal(accountId, response.AccountId);
            Assert.Equal(userId, response.UserId);
        }

        [Fact]
        public void MemberCreationIdempotent()
        {
            // arrange
            var accountId = new Guid("b8d47dce23074128be1d3537b02dcd62");
            var userId = new Guid("c3147dce23074128be1d3537b02dcd22");
            var service = _context.Resolve<CreateMemberService>();
            var countService = _context.Resolve<GetMembersService>();

            // act
            var createResponse1 = service.Post(new CreateMember { AccountId = accountId, UserId = userId });
            var createResponse2 = service.Post(new CreateMember { AccountId = accountId, UserId = userId }); // Second create call with the same id
            var getResponse = countService.Get(new GetMembers { AccountId = accountId });

            // assert
            Assert.NotNull(createResponse1);
            Assert.Equal(accountId, createResponse1.AccountId);
            Assert.Equal(userId, createResponse1.UserId);

            Assert.NotNull(createResponse2);
            Assert.Equal(accountId, createResponse2.AccountId);// Idempotent
            Assert.Equal(userId, createResponse2.UserId);// Idempotent

            Assert.NotNull(getResponse);
            Assert.Equal(1, getResponse.UserIds.Length); // Idempotent
        }

        // [Fact]
        // public void UserIdIsMandatory()
        // {
        //     // arrange
        //     var id = Guid.Empty;
        //     var first = "first";
        //     var last = "last";
        //     var service = _context.Resolve<CreateMemberService>();

        //     // act
        //     Exception ex = Assert.Throws<ArgumentException>(() => service.Post(new CreateMember { Id = id, FirstName = first, LastName = last }));

        //     // assert
        //     Assert.Equal("Id must be supplied", ex.Message);
        // }

    }
}