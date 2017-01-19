using System;
using Services.Account;
using Services.Account.Models;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using Xunit;

namespace Services.Account.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public ServiceStackHost AppHost { get; private set; }

        public DatabaseFixture()
        {
            AppHost = new BasicAppHost().Init();
            var container = AppHost.Container;

            container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            container.RegisterAutoWired<CreateAccountService>();

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<AccountData>();
            }
        }

        public void Dispose()
        {
            AppHost.Dispose();
        }
    }

    public class CreateAccountTests : IClassFixture<DatabaseFixture>
    {
        private DatabaseFixture _fixture;
        public CreateAccountTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CheckServiceCanBeCreated()
        {
            var service = _fixture.AppHost.Container.Resolve<CreateAccountService>();
            var response = service.Post(new CreateAccount { Id = Guid.NewGuid(), Name = "" });
            Console.WriteLine("Response: " + response.ToJson());
        }
    }
}