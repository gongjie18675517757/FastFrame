using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 应用程序初始化时
    /// </summary>
    public interface IApplicationInitialLifetime
    {
        /// <summary>
        /// 执行初始化(只会执行一次)
        /// 注意：要自己处理异常
        /// </summary>
        /// <returns></returns>
        Task InitialAsync();
    }
}
