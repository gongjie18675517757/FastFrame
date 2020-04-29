using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FastFrame.Repository
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var interfaceType = typeof(IUnitOfWork);
            var types = typeof(IUnitOfWork).Assembly.GetTypes()
                .Where(x =>
                    interfaceType.IsAssignableFrom(x) &&
                    x.IsClass &&
                    !x.IsAbstract &&
                    !x.Name.StartsWith("BaseRepository") &&
                    !x.IsGenericType); 

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(BaseQueryable<>));
            services.AddScoped<IUnitOfWork,BaseUnitOfWork>();

            foreach (var type in types)
            {
                foreach (var interfaceItem in type.GetInterfaces().Where(x => x != interfaceType))
                {
                    services.AddScoped(interfaceItem, type);
                } 
            }

            return services;
        }
    }
}
