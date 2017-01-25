using System;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Services.Membership.Tests
{
    public class TestContext : IDisposable
    {
        private Container _container;

        public TestContext()
        {
            _container = new Container();

            _container.Register<IDbConnectionFactory>(new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
            
            _container.RegisterAutoWired<CreateMemberService>();
            _container.RegisterAutoWired<DeleteMemberService>();
            _container.RegisterAutoWired<GetMembersService>();
            _container.RegisterAutoWired<ReadyService>();
            _container.RegisterAutoWired<AliveService>();
            
            using (var db = _container.Resolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<MemberData>();
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