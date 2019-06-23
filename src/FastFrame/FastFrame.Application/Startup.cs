using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using CSRedis;
using FastFrame.Application.Hubs;
using FastFrame.Application.Privder;
using FastFrame.Dto.Dtos.Chat;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.MessageBus;
using FastFrame.Repository;
using FastFrame.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace FastFrame.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
             
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
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            services.AddSignalR();
            services.AddLogging(r => r.AddLog4Net());
            services.AddOptions();
            services.Configure<ResourceOption>(Configuration.GetSection("ResourceOption"));

            services
                .AddHttpContextAccessor()
                .AddDbContextPool<Database.DataBase>(o =>
                {
                    o.UseMySql(Configuration.GetConnectionString("MyDbConnection"), mySqlOptions=> {
                        mySqlOptions.ServerVersion(new Version(5, 6, 40), ServerType.MySql);
                    });
                    //o.UseInMemoryDatabase("Local_Mysql");
                })
                .AddSingleton<ITypeProvider, TypeProvider>()
                .AddScoped<IScopeServiceLoader, ScopeServiceLoader>()
                .AddScoped<ICurrentUserProvider, CurrentUserProvider>()
                .AddScoped<IResourceProvider, ResourceProvider>()
                .AddScoped<IDescriptionProvider, DescriptionProvider>()
                .AddSingleton<IClientManage, ClientConMamage>()
                .AddServices()
                .AddRepository()
                .AddMessageBus(this.GetType().Assembly)
                .AddEventBus(this.GetType().Assembly);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Version = "v1",
                    Title = "API文档",
                    Description = "快速开发平台",
                });
                options.DescribeAllEnumsAsStrings();
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "FastFrame.Application.xml");
                options.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(basePath, "FastFrame.Dto.xml");
                options.IncludeXmlComments(xmlPath); 
            }); 

            services.AddSingleton(x =>
            {
                var redisClient = new CSRedisClient(Configuration.GetConnectionString("RedisConnection"));
                RedisHelper.Initialization(redisClient);
                redisClient.Del("CacheUserMapKey");
                return redisClient;
            });
            var container = services.ToServiceContainer();
            var serviceResolver = container.Build(); 

            return serviceResolver;
        }

        /*master*/

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            app.ApplicationServices.GetService<IMessageBus>().SubscribeAsync<RecMsgOutPut>();
            app.ApplicationServices.GetService<RedisClient>();
            app.ApplicationServices.GetService<Database.DataBase>().Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }  
             

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/hub/chat");
                routes.MapHub<MessageHub>("/hub/message");
            });
            app.UseMiddleware<ResourceMiddleware>();

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
                if (context.Request.Path.HasValue && context.Request.Path.Value == "/")
                    context.Response.Redirect("/index.html");

                await next.Invoke();
            });
            //app.UseRewriter(new RewriteOptions().AddRewrite())

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    app.ApplicationServices.GetRequiredService<IOptions<ResourceOption>>().Value.BasePath),
                RequestPath = "/resource"
            });
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.InjectJavascript("/swagger/ui/Customization.js");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MsSystem API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                app.ApplicationServices.GetService<ILogger<Startup>>().LogInformation("ApplicationStarted");
            });
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetService<ILogger<Startup>>().LogInformation("ApplicationStopped");
            });
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                app.ApplicationServices.GetService<ILogger<Startup>>().LogInformation("ApplicationStopping");
            });
        }
    } 
}
