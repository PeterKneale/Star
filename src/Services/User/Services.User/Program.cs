using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Data;

namespace Services.User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:82/")
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseServiceStack(new AppHost());

            app.Run(context =>
            {
                context.Response.Redirect("/metadata");
                return Task.FromResult(0);
            });
        }
    }


    public class AppHost : AppHostBase
    {
        public AppHost() : base("api", typeof(AppHost).GetAssembly()) { }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ConsoleLogFactory(debugEnabled: true);
            var log = LogManager.GetLogger(typeof(AppHost));            
            
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new CorsFeature());

            SetConfig(new HostConfig { DebugMode = true });
            
            this.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
            {
                log.Error($"Error: {exception.Message}. {exception.StackTrace}.", exception);
                return null;
            });
            
            //var dbFactory = new OrmLiteConnectionFactory("Server=192.168.99.103;Port=32768;Database=postgres;User Id=postgres;", PostgreSqlDialect.Provider);  
            var dbFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            dbFactory.OpenDbConnection().CreateTable<UserData>(true);
            container.Register<IDbConnectionFactory>(c => dbFactory);
        }
    }
}