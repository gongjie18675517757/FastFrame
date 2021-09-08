using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using FastFrame.Application;
using FastFrame.Database;
using FastFrame.Infrastructure.Client;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Infrastructure.Lock;
using FastFrame.Infrastructure.MessageQueue;
using FastFrame.Infrastructure.Module;
using FastFrame.Infrastructure.Permission;
using FastFrame.Infrastructure.Resource;
using FastFrame.Repository;
using FastFrame.WebHost.Hubs;
using FastFrame.WebHost.Middleware;
using FastFrame.WebHost.Privder;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace FastFrame.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string ConnectionName = "Local_Mysql";


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var configurationOptions = StackExchange.Redis.ConfigurationOptions.Parse(Configuration.GetConnectionString("RedisConnection"));
            var connectionMultiplexer = StackExchange.Redis.ConnectionMultiplexer.Connect(configurationOptions);
            services.AddSingleton(connectionMultiplexer);            

            services.AddHangfire(configuration =>
                                    configuration
                                      .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                      .UseSimpleAssemblyNameTypeSerializer()
                                      .UseRecommendedSerializerSettings()  
                                      .UseRedisStorage(connectionMultiplexer, new Hangfire.Redis.RedisStorageOptions
                                      {
                                          Prefix = configurationOptions.ChannelPrefix,
                                          Db = configurationOptions.DefaultDatabase ?? -1,                                             
                                      }));

            services.AddHangfireServer();

            services.AddMvc(opts =>
                {
                    //opts.Filters.Add<GlobalFilter>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services
                .AddSignalR()
                .AddJsonProtocol(config =>
                {
                    config.PayloadSerializerOptions.PropertyNamingPolicy = null;
                    config.PayloadSerializerOptions.IgnoreNullValues = false;
                    config.PayloadSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                });

            services.AddLogging(r => r.AddLog4Net());
            services.AddOptions();
            services.Configure<ResourceOption>(Configuration.GetSection("ResourceOption"));
            services.AddMemoryCache();
            services.AddSession();

            services
                .AddHttpContextAccessor()
                .AddDbContextPool<DataBase>(o =>
                {
                    var conn_str = Configuration.GetConnectionString(ConnectionName);
                    o.UseMySql(conn_str, ServerVersion.Parse("5.6.40"), opt =>
                    {
                        opt.CommandTimeout(60);
                    })
                    .AddInterceptors(new FmtCommandInterceptor());
                })
                .AddScoped<IModuleExportProvider, ModuleExportProvider>()
                .AddScoped<IApplicationSession, AppSessionProvider>()
                .AddTransient<IApplicationInitialLifetime, ApplicationInitialProvider>()
                .AddScoped<IResourceProvider, ResourceProvider>()
                .AddScoped<IModuleDesProvider, XmlModuleDesProvider>()
                .AddScoped<IExcelExportProvider, ExcelExportProvider>()
                .AddScoped<IPermissionDefinitionContext, PermissionDefinitionContext>()
                .AddScoped<IEventBus, EventBus>()
                .AddSingleton<IClientManage, ClientMamager>()
                .AddSingleton<IClientConnection, ClientConnection>()
                .AddSingleton<ICacheProvider, CacheProvider>()
                .AddSingleton<ILockFacatory, CacheProvider>()
                .AddSingleton<IBackgroundJob, HangfireBackgroundJob>()
                .AddSingleton<IMessageQueue, MessageQueue>()
                .AddSingleton<IApplicationInitialLifetime>(v => (MessageQueue)v.GetService<IMessageQueue>())
                .AddSingleton<IApplicationUnInitialLifetime>(v => (MessageQueue)v.GetService<IMessageQueue>())
                .AddServices()
                .AddRepository()
                .AddIntervalWork(typeof(IService).Assembly, typeof(Startup).Assembly, typeof(Infrastructure.Extension).Assembly)
                .AddMessageQueue(typeof(IService).Assembly, typeof(Startup).Assembly, typeof(Infrastructure.Extension).Assembly)
                ;
            services.AddSingleton<LockMethodAttribute>();

            /*添加动态代理*/
            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<LockMethodAttribute>(Predicates.ForService("*Service"));
            });
#if DEBUG
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API文档",
                    Description = "快速开发平台",
                });
                options.UseInlineDefinitionsForEnums();
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "FastFrame.WebHost.xml");
                options.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(basePath, "FastFrame.Application.xml");
                options.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(basePath, "FastFrame.Entity.xml");
                options.IncludeXmlComments(xmlPath);
            });
#endif  
        }

        /*master*/

        public void Configure(IApplicationBuilder app, IHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

#if DEBUG
                /*注册swagger*/
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MsSystem API V1");
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });
#endif
            }

            //app.UseRewriter(new RewriteOptions().AddRewrite()) 

            /*使用静态文件*/
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles(); 

            /*记录接口请求时间*/
            app.UseMiddleware<InvodeTimeMiddleware>();

            /*使用Session*/
            app.UseSession();

            /*初始化应用会话状态*/
            app.UseMiddleware<AppSessionInitMiddleware>();

            /*异步处理中间件*/
            app.UseMiddleware<ExceptionMiddleware>();

            /*处理资源文件*/
            app.UseMiddleware<ResourceMiddleware>();

            /*注册路由*/
            app.UseRouting();

            /*授权验证中间件*/
            app.UseMiddleware<AuthorizationMiddleware>();

            /*权限验证中间件*/
            app.UseMiddleware<PermissionMiddleware>();

            /*注册终结点*/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/hub/message");
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });




            var logger = app.ApplicationServices.GetService<ILogger<Startup>>();

            applicationLifetime.ApplicationStarted.Register(async () =>
            {
                logger.LogInformation("ApplicationStarted");
                logger.LogWarning("ApplicationStarted");
                logger.LogDebug("ApplicationStarted");
                logger.LogError("ApplicationStarted");
                logger.LogTrace("ApplicationStarted");
                logger.LogCritical("ApplicationStarted");

                /*初始化应用*/
                var applicationInitialProviders = app.ApplicationServices.GetServices<IApplicationInitialLifetime>();
                foreach (var applicationInitialProvider in applicationInitialProviders)
                    await applicationInitialProvider.InitialAsync();

                //BackgroundJob.Enqueue(() => Console.WriteLine(DateTime.Now));
                //var v1 = Infrastructure.Extension.ToObject<int>("1");
                //var v2 = Infrastructure.Extension.ToObject<bool>("true");
                //RecurringJob.AddOrUpdate(() => Console.WriteLine($"{v1} {v2}"), Cron.Minutely);

            });

            applicationLifetime.ApplicationStopping.Register(async () =>
            {
                logger.LogInformation("ApplicationStopping");

                /*处理应用关闭时事件*/
                var applicationInitialProviders = app.ApplicationServices.GetServices<IApplicationUnInitialLifetime>();
                foreach (var applicationInitialProvider in applicationInitialProviders)
                {
                    await applicationInitialProvider.UnInitialAsync();
                }

                /*关闭redis*/
                await app.ApplicationServices.GetService<StackExchange.Redis.ConnectionMultiplexer>().CloseAsync();
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                logger.LogInformation("ApplicationStopped");
            });
        }
    }
}
