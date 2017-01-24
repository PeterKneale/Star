using System;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.User.Tests
{
    public class TestContext : IDisposable
    {
        private Container _container;

        public TestContext()
        {
            _container = new Container();

            _container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            
            _container.RegisterAutoWired<CountUsersService>();
            _container.RegisterAutoWired<CreateUserService>();
            _container.RegisterAutoWired<DeleteUserService>();
            _container.RegisterAutoWired<GetUserService>();
            _container.RegisterAutoWired<GetUsersService>();
            _container.RegisterAutoWired<UpdateUserService>();
            _container.RegisterAutoWired<AliveService>();
            _container.RegisterAutoWired<ReadyService>();

            using (var db = _container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<UserData>();
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