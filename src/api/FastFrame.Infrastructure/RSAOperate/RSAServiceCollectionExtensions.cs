using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FastFrame.Infrastructure.RSAOperate
{
    public static class RSAServiceCollectionExtensions
    {
        public static IServiceCollection AddRSAProvider(this IServiceCollection services, IConfiguration configurationSection)
        {
            services.AddSingleton<RSAProvider>();
            services.Configure<RSAConfig>(configurationSection);
            return services;
        }
    }
}
