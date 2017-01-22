using System;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Account.Tests
{
    public class TestContext : IDisposable
    {
        private Container _container;

        public TestContext()
        {
            _container = new Container();

            _container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            // TODO: Figure out a way to remove this
            _container.RegisterAutoWired<CountAccountsService>();
            _container.RegisterAutoWired<CreateAccountService>();
            _container.RegisterAutoWired<DeleteAccountService>();
            _container.RegisterAutoWired<GetAccountService>();
            _container.RegisterAutoWired<GetAccountsService>();
            _container.RegisterAutoWired<AliveService>();
            _container.RegisterAutoWired<ReadyService>();

            using (var db = _container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<AccountData>();
            }
        }
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}