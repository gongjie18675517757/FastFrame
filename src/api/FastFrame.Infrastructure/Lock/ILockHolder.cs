using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Lock
{
    /// <summary>
    /// 锁的持有者
    /// </summary>
    public interface ILockHolder : IDisposable,IAsyncDisposable
    {
    }
}
