using FastFrame.Infrastructure.Cache;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.Hosting;

namespace FastFrame.Infrastructure.Lock
{
    public class LockFacatoryProvider : ILockFacatory
    {
        private readonly IDistributedLockProvider distributedLockProvider;

        public LockFacatoryProvider(StackExchange.Redis.ConnectionMultiplexer multiplexer)
        {
            distributedLockProvider = new RedisDistributedSynchronizationProvider(multiplexer.GetDatabase());
        }

        public async Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan delayTime)
        {
            var redisDistributedLockHandle = await distributedLockProvider.TryAcquireLockAsync(key, delayTime);
            if (redisDistributedLockHandle == null) return null;

            return new LockHolder(redisDistributedLockHandle);
        } 


        /// <summary>
        /// 锁的持有者
        /// </summary>
        private class LockHolder : ILockHolder, IDisposable
        {
            private readonly IDistributedSynchronizationHandle distributedSynchronizationHandle;

            public LockHolder(IDistributedSynchronizationHandle distributedSynchronizationHandle)
            {
                this.distributedSynchronizationHandle = distributedSynchronizationHandle;
            }

            public void Dispose()
            {
                distributedSynchronizationHandle.Dispose();
            }

            public ValueTask DisposeAsync()
            {
                return distributedSynchronizationHandle.DisposeAsync();
            }
        }
    }
}
