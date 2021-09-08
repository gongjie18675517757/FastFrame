using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Lock
{
    public interface ILockHolder
    {
        /// <summary>
        /// 释放锁
        /// </summary>
        /// <returns></returns>
        Task LockRelease();
    }
}
