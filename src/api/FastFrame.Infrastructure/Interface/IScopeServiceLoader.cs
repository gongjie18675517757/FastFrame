using System;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 服务加载器[IServiceProvider]
    /// </summary>
    public interface IScopeServiceLoader
    {
        T GetService<T>();

        object GetService(Type type);
    }
}
