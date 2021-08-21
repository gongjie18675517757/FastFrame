using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 应用程序关闭时
    /// </summary>
    public interface IApplicationUnInitialLifetime
    {
        /// <summary>
        /// 执行关闭操作(只会执行一次)
        /// 注意：要自己处理异常
        /// </summary>
        /// <returns></returns>
        Task UnInitialAsync();
    }
}
