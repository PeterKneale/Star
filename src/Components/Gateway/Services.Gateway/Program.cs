﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Web;
using System.Collections.Generic;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;

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
            
            Plugins.Add(new AuthFeature(() => new CustomAuthUserSession(),
                new IAuthProvider[] {
                    new JwtAuthProviderReader(AppSettings) {
                        Audience = "simplicate.net",
                        HashAlgorithm = "HS256",
                        AuthKeyBase64 = "cXdlcnR5dWlvcGFzZGZnaGprbHp4Y3Zibm0xMjM0NTY=",
                        RequireSecureConnection = false,
                        
                        PopulateSessionFilter = (session, jwtJson, req) =>
                        {
                            var accountId = Guid.Parse(jwtJson["AccountId"]);
                            var userId = Guid.Parse(jwtJson["UserId"]);

                            var customSession = session as CustomAuthUserSession;
                            customSession.AccountId = accountId;
                            customSession.UserId = userId;
                            session = customSession;
                        }
                    }
                }){
                    IncludeAssignRoleServices = false
                });


            SetConfig(new HostConfig { DebugMode = true });

            AppSettings = new MultiAppSettings(new EnvironmentVariableSettings(), new AppSettings());

            container.Register<IServiceGatewayFactory>(x => new CustomServiceGatewayFactory())
                .ReusedWithin(ReuseScope.None);
            
            container.Register<IAuthRepository>(c => new OrmLiteAuthRepository(
                c.Resolve<IDbConnectionFactory>()));

            this.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
            {
                log.Error($"Error: {exception.Message}. {exception.StackTrace}.", exception);
                return null;
            });
        }
    }

    public class CustomAuthUserSession : AuthUserSession
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
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
                {"Membership","http://localhost:83"},
                {"Account","http://localhost:81"},
                {"User","http://localhost:82"}
            };

            foreach (var service in services.Keys)
            {
                if (ServiceHandles(service, requestType))
                {
                    var address = services[service];
                    var request = requestType.Name;
                    Console.WriteLine(string.Format("Routing '{0}' to '{1}' service at '{2}'", request, service, address));

                    return address;
                }
            }

            throw new NotSupportedException("Couldn't figure out the endpoint");
        }
        private bool ServiceHandles(string service, Type requestType)
        {
            return requestType.FullName.Contains("Services." + service + ".Models");
        }
    }
}