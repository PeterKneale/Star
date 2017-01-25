using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Messaging;
using ServiceStack.Web;
using System.Collections.Generic;
using ServiceStack.Auth;

namespace Services.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls($"http://localhost:80/")
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

            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                    new JwtAuthProvider(AppSettings) { AuthKey = AesUtils.CreateKey() },
                    new CredentialsAuthProvider(AppSettings)
                }));

            SetConfig(new HostConfig { DebugMode = true });

            container.Register<IServiceGatewayFactory>(x => new CustomServiceGatewayFactory())
                .ReusedWithin(ReuseScope.None);

            this.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
            {
                log.Error($"Error: {exception.Message}. {exception.StackTrace}.", exception);
                return null;
            });
        }
    }


    public class CustomServiceGatewayFactory : ServiceGatewayFactoryBase
    {
        public override IServiceGateway GetGateway(Type requestType)
        {
            var isLocal = HostContext.Metadata.RequestTypes.Contains(requestType);
            var gateway = isLocal
                ? (IServiceGateway)base.localGateway
                : new JsonServiceClient(GetEndpoint(requestType));
            return gateway;
        }

        private string GetEndpoint(Type requestType)
        {
            var services = new Dictionary<string, string>{
                {"Services.Membership","http://localhost:83"},
                {"Services.Account","http://localhost:81"},
                {"Services.User","http://localhost:82"}
            };
            foreach (var serviceName in services.Keys)
            {
                if (requestType.FullName.Contains(serviceName))
                {
                    var url = services[serviceName];
                    Console.WriteLine(string.Format("Routing {0} to {1} Service at {2}", requestType.FullName, serviceName, url));
                    return url;
                }
            }
            throw new NotSupportedException("Couldn't figure out the endpoint");
        }
    }
}