using FastFrame.Application.Hubs;
using FastFrame.Application.Privder;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using FastFrame.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using AspectCore.DynamicProxy;

namespace FastFrame.Application
{
    public class Startup
    {
        private SubscribeBus subscriber;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies 
                // is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(opts =>
            {
                opts.Filters.Add<GlobalFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter() { CamelCaseText = true });
                });
            services.AddSignalR();

            ILoggerFactory Mlogger = new LoggerFactory()
               .AddDebug((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name))
              .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

            services.AddOptions();

            services.Configure<ResourceOption>(Configuration.GetSection("ResourceOption"));
            //services.Configure<RedisConfig>(Configuration.GetSection("RedisConfig"));

            services
                .AddHttpContextAccessor()
                .AddDbContextPool<Database.DataBase>(o =>
                {
                    o.UseMySql(Configuration.GetConnectionString("Local_Mysql")).UseLoggerFactory(Mlogger);
                    //o.UseInMemoryDatabase("Local_Mysql");
                })
                .AddSingleton<ITypeProvider, TypeProvider>()
                .AddScoped<IScopeServiceLoader, ScopeServiceLoader>()
                .AddScoped<ICurrentUserProvider, CurrentUserProvider>()
                .AddScoped<IResourceProvider, ResourceProvider>()
                .AddScoped<IDescriptionProvider, DescriptionProvider>()
                .AddSingleton<IMessageBus, MessageBusProvider>()
                .AddServices()
                .AddRepository()
                .AddEventBus(this.GetType().Assembly);

            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "API文档",
                    Description = "快速开发平台API文档",
                    TermsOfService = "None",
                });
                options.DescribeAllEnumsAsStrings();
                ////Determine base path for the application.  
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                ////Set the comments path for the swagger json and ui.  
                //var xmlPath = Path.Combine(basePath, "MsSystem.API.xml");
                //options.IncludeXmlComments(xmlPath);
            });

            var client = new CSRedis.CSRedisClient("127.0.0.1");
            RedisHelper.Initialization(client);
            string CacheUserMapKey = "CacheUserMapKey";
            client.Del(CacheUserMapKey);
            services.AddSingleton(client);
            var container = services.ToServiceContainer();
            var serviceResolver = container.Build();
            subscriber = new SubscribeBus(client, serviceResolver);
            subscriber.Start();
            return serviceResolver;
        }

        /*master*/

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/hub/chat");
                routes.MapHub<MessageHub>("/hub/message");
            });
            app.Use(async (context, next) =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                context.Response.OnStarting(() =>
                {
                    stopwatch.Stop();
                    context.Response.Headers.Add("ElapsedMilliseconds", stopwatch.ElapsedMilliseconds.ToString());
                    return Task.CompletedTask;
                });
                if (context.Request.Path.Value == "/")
                    context.Response.Redirect("/index.html");
                await next.Invoke();
            });
            app.UseStaticFiles();
            app.UseMvc();
            //app.UseRouter(r =>
            //{
            //    r.MapRoute("defaultApi", "api/{organize?}/{controller=values}/{action}/{id?}/");
            //});
            app.UseSwagger();
            app.UseSwaggerUi();

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("ApplicationStarted");
            });
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("ApplicationStopped");
            });
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("ApplicationStopping");
            });
        }
    }
}
