using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Lock
{
    public interface ILockFacatory
    {
        /// <summary>
        /// 尝试创建锁,如果资源被占用,则返回NULL
        /// </summary>
        /// <param name="key">资源键</param>
        /// <param name="timeSpan">锁定时长</param>
        /// <param name="hasWatchdog">是否需要看门狗</param>
        /// <returns></returns>
        Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan timeSpan, bool hasWatchdog);
    }
}
