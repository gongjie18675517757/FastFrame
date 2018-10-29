using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFrame.Application.Hubs;
using FastFrame.Application.Privder;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using FastFrame.Service;
using FastFrame.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {                   
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

            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddDbContextPool<Database.DataBase>(o =>
                {
                    o.UseMySql(Configuration.GetConnectionString("Local_Mysql")).UseLoggerFactory(Mlogger);
                })
                .AddSingleton<ITypeProvider, TypeProvider>()
                .AddScoped<IScopeServiceLoader, ScopeServiceLoader>()  
                .AddScoped<ICurrentUserProvider, CurrentUserProvider>() 
                .AddServices()
                .AddRepository();

            return services.BuildServiceProvider();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/hub/chat");
            });

            app.UseMvc(r =>
            {
                r.MapRoute("defaultApi", "api/{organize?}/{controller=values}/{action}/{id?}/");
            });
        }
    }
}
