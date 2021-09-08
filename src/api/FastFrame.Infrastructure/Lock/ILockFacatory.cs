using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Lock
{
    public interface ILockFacatory
    {
        Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan timeSpan);
    }
}
