using FastFrame.Infrastructure.Cache;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Lock
{
    public interface ILockFacatory
    {
        /// <summary>
        /// 尝试创建锁,如果资源被占用,则返回NULL
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan timeSpan);
    }

    public class LockFacatoryProvider: ILockFacatory
    {
        private readonly ConnectionMultiplexer redisClient;
        private readonly int defaultDatabase;
        private readonly string prefix;

        public LockFacatoryProvider(StackExchange.Redis.ConnectionMultiplexer redisClient)
        {
            this.redisClient = redisClient;

            var configurationOptions = ConfigurationOptions.Parse(redisClient.Configuration);
            defaultDatabase = configurationOptions.DefaultDatabase ?? -1;
            prefix = configurationOptions.ChannelPrefix;
        }
        private StackExchange.Redis.IDatabase GetDatabase()
        {
            return redisClient.GetDatabase(defaultDatabase);
        }


        public async Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan timeSpan)
        {
            key = CacheProvider.ConvertKey(key, prefix);
            var token = Guid.NewGuid().ToString("d");
            var database =  GetDatabase();

            var exists = await database.LockTakeAsync(key, token, timeSpan);

            if (exists)
                return new LockHolder(key, token, database);

            return null;
        }
    }
}
