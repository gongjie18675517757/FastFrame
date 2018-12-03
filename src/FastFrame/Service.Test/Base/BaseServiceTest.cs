using FastFrame.Database;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using FastFrame.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Service.Test
{
    public abstract class BaseServiceTest:System.IDisposable
    {
        public BaseServiceTest()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");
            Configuration = builder.Build();


            IServiceCollection services = new ServiceCollection();
            services
                .AddDbContextPool<DataBase>(o =>
                {
                    o.UseMySql(Configuration.GetConnectionString("Local_Mysql"));
                })
                .AddScoped<IScopeServiceLoader, ScopeServiceLoader>()
                .AddScoped<ICurrentUserProvider, CurrentUserProvider>()
                .AddScoped<IEventBus,EventBus>()
                .AddServices()
                .AddRepository();

             

            serviceScope = services.BuildServiceProvider().CreateScope();
            ServiceProvider= serviceScope.ServiceProvider;
        }

        public IConfigurationRoot Configuration { get; } 

        private IServiceScope serviceScope;

        public IServiceProvider ServiceProvider { get; }

        public virtual void Dispose()
        {
            serviceScope.Dispose();
        }
    }
}
