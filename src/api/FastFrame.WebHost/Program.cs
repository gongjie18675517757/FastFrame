global using AspectCore.Extensions.DependencyInjection;
using FastFrame.Application;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
using FastFrame.Infrastructure.Client;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Identity;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Infrastructure.Lock;
using FastFrame.Infrastructure.MessageQueue;
using FastFrame.Infrastructure.Module;
using FastFrame.Infrastructure.Permission;
using FastFrame.Infrastructure.Resource;
using FastFrame.Infrastructure.RSAOperate;
using FastFrame.Repository;
using FastFrame.WebHost.Hubs;
using FastFrame.WebHost.Middleware;
using FastFrame.WebHost.Privder;
using Hangfire;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

#region .NET5的写法 
//namespace FastFrame.WebHost
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 /*替换成AspectCore的容器实现，性能更高*/
//                 .UseServiceProviderFactory(new ServiceContextProviderFactory())
//                 //.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory())
//                 .ConfigureAppConfiguration((context, config) =>
//                  {
//                      config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//                  })                 
//                 .ConfigureWebHostDefaults(webBuilder =>
//                   { 
//                       webBuilder.UseStartup<Startup>();
//                   });
//    } 
//} 
#endregion

#region 原Program的内容 
var builder = WebApplication.CreateBuilder(args);
/*替换成AspectCore的容器实现，性能更高*/
builder.Host.UseServiceProviderFactory(new ServiceContextProviderFactory());
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
#endregion

#region ConfigureServices 
var Configuration = builder.Configuration;
const string ConnectionName = "Local_Mysql";
var services = builder.Services;

services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//services.Configure<IISServerOptions>(options =>
//{
//    options.AutomaticAuthentication = false;
//});


var configurationOptions = StackExchange.Redis.ConfigurationOptions.Parse(Configuration.GetConnectionString("RedisConnection"));
var connectionMultiplexer = StackExchange.Redis.ConnectionMultiplexer.Connect(configurationOptions);
services.AddSingleton(connectionMultiplexer);

services
    .AddHangfire(configuration =>
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

//builder.Services.AddControllers();
services
    .AddMvc(opts =>
    {
        //opts.Filters.Add<GlobalFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

services
    .AddSignalR()
    .AddJsonProtocol(config =>
    {
        config.PayloadSerializerOptions.PropertyNamingPolicy = null;
        config.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        config.PayloadSerializerOptions.Converters.Add(item: new JsonStringEnumConverter());
    });

services.AddLogging(r => r.AddLog4Net());
services.AddOptions();
services.Configure<ResourceOption>(Configuration.GetSection("ResourceOption"));
services.Configure<IdentityConfig>(Configuration.GetSection("IdentityConfig"));
services.AddMemoryCache();
services.AddSession();

services
    .AddHttpContextAccessor()
    .AddDbContextPool<FastFrame.Database.DataBase>(o =>
    {
        var conn_str = Configuration.GetConnectionString(ConnectionName);
        o.UseMySql(conn_str,
                   ServerVersion
#if DEBUG
                    .Parse("5.6.40"),
#else
                    .Parse("8.0.28"), 
#endif 
                   opt =>
                    {
                        opt.CommandTimeout(60);
                    })
        .AddInterceptors(new FastFrame.Database.FmtCommandInterceptor());
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
    .AddSingleton<ILockFacatory, LockFacatoryProvider>()
    .AddSingleton<IBackgroundJob, HangfireBackgroundJob>()
    .AddSingleton<MessageQueue>()
    .AddSingleton<IMessageQueue>(s => s.GetService<MessageQueue>())
    .AddSingleton<LockMethodAttribute>()
    .AddSingleton<IApplicationInitialLifetime>(v => v.GetService<MessageQueue>())
    .AddSingleton<IApplicationUnInitialLifetime>(v => v.GetService<MessageQueue>())
    .AddServices()
    .AddRepository()
    .AddRSAProvider(Configuration.GetSection("rsaConfig"))
    .AddIntervalWork(typeof(IService).Assembly, typeof(Program).Assembly, typeof(FastFrame.Infrastructure.Extension).Assembly)
    .AddMessageQueue(typeof(IService).Assembly, typeof(Program).Assembly, typeof(FastFrame.Infrastructure.Extension).Assembly);



/*添加动态代理*/
services.ConfigureDynamicProxy(config =>
{
    //config.Interceptors.AddTyped<LockMethodAttribute>(Predicates.ForService("*Service"));
});

//#if DEBUG
builder.Services.AddEndpointsApiExplorer();

var basePath = AppDomain.CurrentDomain.BaseDirectory;
var areas = typeof(Program)
       .Assembly
       .GetTypes()
       .Where(v => v.IsClass && !v.IsAbstract && typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(v))
       .Select(v => new
       {
           name = v.Namespace.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
           text = T4Help.GetClassSummary(v, basePath)
       })
       .GroupBy(v => v.name)
       .Select(v => v.FirstOrDefault())
       .ToArray();

#if DEBUG
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "接口文档",
        Version = "v1",
        Description = "测试 webapi"
    });



    var xmlPath = Path.Combine(basePath, "FastFrame.WebHost.xml");
    options.IncludeXmlComments(xmlPath);
    xmlPath = Path.Combine(basePath, "FastFrame.Infrastructure.xml");
    options.IncludeXmlComments(xmlPath);
    xmlPath = Path.Combine(basePath, "FastFrame.Entity.xml");
    options.IncludeXmlComments(xmlPath);
    xmlPath = Path.Combine(basePath, "FastFrame.Application.xml");
    options.IncludeXmlComments(xmlPath);


    options.UseInlineDefinitionsForEnums();

    foreach (var item in areas)
    {
        options.SwaggerDoc(item.name, new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = item.text,
            Version = "v1",
            Description = item.text
        });
    }

    options.DocInclusionPredicate((docName, apiDescription) =>
    {
        if (apiDescription.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor caDescriptor)
        {
            var area = caDescriptor.ControllerTypeInfo.Namespace.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            return docName == area || !areas.Any(v => v.name == docName);
        }

        return true;
    });

    options.AddSecurityDefinition(ConstValuePool.Token_Name, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "登录后返回的token",
        Name = ConstValuePool.Token_Name,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Scheme",

    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=ConstValuePool.Token_Name
                }
            },
            Array.Empty<string>()
        }
    });
});
#endif
#endregion

#region Configure 
var app = builder.Build();
var env = app.Environment;
var applicationLifetime = app.Lifetime;

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

#if DEBUG
/*注册swagger*/
app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MsSystem API V1");
//    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

//    foreach (var item in areas)
//        c.SwaggerEndpoint($"/swagger/{item}/swagger.json", item);
//});
//#endif

app.UseRewriter(new RewriteOptions().AddRewrite("swagger/index.html", "index.html", false));
app.UseRewriter(new RewriteOptions().AddRewrite("swagger", "index.html", false));
#endif



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

/*处理资源文件*/
app.UseMiddleware<ResourceMiddleware>();

/*异步处理中间件*/
app.UseMiddleware<ExceptionMiddleware>();

/*注册路由*/
app.UseRouting();

/*授权验证中间件*/
app.UseMiddleware<AuthorizationMiddleware>();

/*权限验证中间件*/
app.UseMiddleware<PermissionMiddleware>();

/*注册终结点*/
app.MapHub<MessageHub>("/hub/message");
app.MapControllers();
app.MapHangfireDashboard();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<MessageHub>("/hub/message");
//    endpoints.MapControllers();
//    endpoints.MapHangfireDashboard();


//    //endpoints.MapControllerRoute(
//    //    name: "default",
//    //    pattern: "{controller=Home}/{action=Index}/{id?}");
//});

var logger = app.Services.GetService<ILogger<Program>>();

applicationLifetime.ApplicationStarted.Register(async () =>
{
    logger.LogInformation("ApplicationStarted");
    logger.LogWarning("ApplicationStarted");
    logger.LogDebug("ApplicationStarted");
    logger.LogError("ApplicationStarted");
    logger.LogTrace("ApplicationStarted");
    logger.LogCritical("ApplicationStarted");

    /*初始化应用*/
    var applicationInitialProviders = app.Services.GetServices<IApplicationInitialLifetime>();
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
    var applicationInitialProviders = app.Services.GetServices<IApplicationUnInitialLifetime>();
    foreach (var applicationInitialProvider in applicationInitialProviders)
    {
        await applicationInitialProvider.UnInitialAsync();
    }

    /*关闭redis*/
    await app.Services.GetService<StackExchange.Redis.ConnectionMultiplexer>().CloseAsync();
});

applicationLifetime.ApplicationStopped.Register(() =>
{
    logger.LogInformation("ApplicationStopped");
});
#endregion

app.Run();