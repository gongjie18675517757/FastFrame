using CSRedis;
using FastFrame.Application;
using FastFrame.Database;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.MessageBus;
using FastFrame.Infrastructure.Module;
using FastFrame.Infrastructure.Permission;
using FastFrame.Repository;
using FastFrame.WebHost.Hubs;
using FastFrame.WebHost.Privder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace FastFrame.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<IISServerOptions>(config =>
            {
                config.AutomaticAuthentication = false;
            });
            services.AddMvc(opts =>
            {
                opts.Filters.Add<GlobalFilter>();
            }).AddNewtonsoftJson(options =>
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

            services
                .AddHttpContextAccessor()
                .AddDbContextPool<Database.DataBase>(o =>
                {
                    o.UseMySql(Configuration.GetConnectionString("MyDbConnection"), mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(5, 6, 40), ServerType.MySql);
                    }).AddInterceptors(new FmtCommandInterceptor());
                })
                .AddScoped<IModuleExportProvider, ModuleExportProvider>()
                .AddScoped<IAppSessionProvider, AppSessionProvider>()
                .AddScoped<IApplicationInitialProvider, ApplicationInitialProvider>()
                .AddScoped<IResourceProvider, ResourceProvider>()
                .AddScoped<IModuleDesProvider, XmlModuleDesProvider>()
                .AddScoped<IExcelExportProvider, ExcelExportProvider>()
                .AddScoped<IPermissionDefinitionContext, PermissionDefinitionContext>()
                .AddSingleton<IClientManage, ClientConMamage>()
                .AddServices()
                .AddRepository()
                .AddSingleton<IMessageBus, MessageBus>()
                .AddScoped<IEventBus, EventBus>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
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

            services.AddSingleton(x =>
            {
                var redisClient = new CSRedisClient(Configuration.GetConnectionString("RedisConnection"));
                RedisHelper.Initialization(redisClient);
                redisClient.Del("CacheUserMapKey");
                return redisClient;
            });
        }

        /*master*/

        public void Configure(IApplicationBuilder app, IHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await context.Request.HttpContext.RequestServices.GetService<IAppSessionProvider>().InitAsync();

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                context.Response.OnStarting(() =>
                {
                    stopwatch.Stop();
                    context.Response.Headers.Add("ElapsedMilliseconds", stopwatch.ElapsedMilliseconds.ToString());
                    return Task.CompletedTask;
                });

                if (context.Request.Path.HasValue && context.Request.Path.Value == "/")
                    context.Response.Redirect("/index.html");

                await next.Invoke();
            });
            //app.UseRewriter(new RewriteOptions().AddRewrite()) 

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ResourceMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<ChatHub>("/hub/chat");
                endpoints.MapHub<MessageHub>("/hub/message");
                endpoints.MapControllers();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MsSystem API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
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

                var applicationInitialProviders = app.ApplicationServices.GetServices<IApplicationInitialProvider>();
                foreach (var applicationInitialProvider in applicationInitialProviders)
                {
                    await applicationInitialProvider.InitialAsync();
                } 
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                logger.LogInformation("ApplicationStopped");
            });

            applicationLifetime.ApplicationStopping.Register(async () =>
            {
                logger.LogInformation("ApplicationStopping");

                var applicationInitialProviders = app.ApplicationServices.GetServices<IApplicationUnInitialProvider>();
                foreach (var applicationInitialProvider in applicationInitialProviders)
                {
                    await applicationInitialProvider.UnInitialAsync();
                }
            });
        }
    }
}
