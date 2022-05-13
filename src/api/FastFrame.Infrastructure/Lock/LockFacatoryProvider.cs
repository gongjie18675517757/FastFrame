using FastFrame.Infrastructure.Cache;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace FastFrame.Infrastructure.Lock
{
    public class LockFacatoryProvider : BackgroundService, ILockFacatory
    {
        private readonly ConnectionMultiplexer redisClient;
        private readonly int defaultDatabase;
        private readonly string prefix;
        private readonly ConcurrentDictionary<string, LockHolder> keyValuePairs = new(concurrencyLevel: Environment.ProcessorCount * 2, capacity: 101);

        public LockFacatoryProvider(ConnectionMultiplexer redisClient)
        {
            Console.WriteLine("LockFacatoryProvider init");

            this.redisClient = redisClient;

            var configurationOptions = ConfigurationOptions.Parse(redisClient.Configuration);
            defaultDatabase = configurationOptions.DefaultDatabase ?? -1;
            prefix = configurationOptions.ChannelPrefix;
        }

        private IDatabase GetDatabase()
        {
            return redisClient.GetDatabase(defaultDatabase);
        }


        public async Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan timeSpan, bool hasWatchdog)
        {
            key = CacheProvider.ConvertKey(key, prefix);
            var token = Guid.NewGuid().ToString("d");
            var database = GetDatabase();

            var exists = await database.LockTakeAsync(key, token, timeSpan);

            if (exists)
            {
                var locker = new LockHolder(key, token, LockRelease);
                if (hasWatchdog)
                    keyValuePairs.TryAdd(token, locker);
                return locker;
            }

            return null;
        }


        private void LockRelease(LockHolder lockHolder)
        {
            keyValuePairs.TryRemove(lockHolder.Token, out _);

            var database = GetDatabase();
            database.LockRelease(lockHolder.Key, lockHolder.Token);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var database = GetDatabase();
            while (!stoppingToken.IsCancellationRequested)
            {
                var list = keyValuePairs.Values.ToList();
                foreach (var item in list)
                {
                    var liveTime = await database.KeyTimeToLiveAsync(item.Key);
                    if (liveTime != null)
                        await database.KeyExpireAsync(item.Key, liveTime.Value.Add(TimeSpan.FromSeconds(2)));
                }

                await Task.Delay(1 * 1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            var list = keyValuePairs.Values.ToList(); 
            foreach (var item in list)
                LockRelease(item);
        }

        /// <summary>
        /// 锁的持有者
        /// </summary>
        private class LockHolder : ILockHolder, IDisposable
        {
            private Action<LockHolder> releaseFunc;

            public string Key { get; }

            public string Token { get; }

            public LockHolder(string key, string token, Action<LockHolder> releaseFunc)
            {
                this.Key = key;
                this.Token = token;
                this.releaseFunc = releaseFunc;
            }

            public void LockRelease()
            {
                releaseFunc?.Invoke(this);
                releaseFunc = null;
            }

            public void Dispose()
            {
                releaseFunc?.Invoke(this);
                releaseFunc = null;
            }
        }
    }
}
