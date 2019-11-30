using FastFrame.Infrastructure.Interface;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Test
{
    public class ScopeServiceLoader : IScopeServiceLoader
    {
        private readonly IServiceProvider serviceProvider;

        public ScopeServiceLoader(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public object GetService(Type type)
        {
            return serviceProvider.GetService(type);
        }
    }
}
