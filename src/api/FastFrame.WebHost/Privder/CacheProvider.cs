using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Lock;
using System;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{

    public class CacheProvider : ICacheProvider, ILockFacatory
    {
        private readonly StackExchange.Redis.ConnectionMultiplexer redisClient;
        private readonly int defaultDatabase;
        private readonly string prefix;

        public CacheProvider(StackExchange.Redis.ConnectionMultiplexer redisClient)
        {
            this.redisClient = redisClient;
            var configurationOptions = StackExchange.Redis.ConfigurationOptions.Parse(redisClient.Configuration);
            defaultDatabase = configurationOptions.DefaultDatabase ?? -1;
            prefix = configurationOptions.ChannelPrefix;
        }


        private static T ConvertValue<T>(StackExchange.Redis.RedisValue value)
        {
            if (!value.HasValue)
                return default;

            return value.ToString().ToObject<T>();
        }


        private string ConvertKey(string key)
        {
            if (prefix.IsNullOrWhiteSpace())
                return key;

            return $"{prefix}{key}";
        }

        public async Task<ILockHolder> TryCreateLockAsync(string key, TimeSpan timeSpan)
        {
            key = ConvertKey(key);
            var token = Guid.NewGuid().ToString("d");
            var database = redisClient.GetDatabase(defaultDatabase);

            var exists = await database.LockTakeAsync(key, token, timeSpan);

            if (exists)
                return new LockHolder(key, token, database);

            return null;
        }

        public Task DelAsync(string key)
        {

            return redisClient.GetDatabase(defaultDatabase).KeyDeleteAsync(ConvertKey(key));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await redisClient.GetDatabase(defaultDatabase).StringGetAsync(ConvertKey(key));
            return ConvertValue<T>(value);
        }

        public async Task<T> HGetAsync<T>(string key, string field)
        {
            try
            {
                var value = await redisClient.GetDatabase(defaultDatabase).HashGetAsync(ConvertKey(key), field);
                return ConvertValue<T>(value);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task HSetAsync<T>(string key, string field, T val, TimeSpan? expire)
        {
            await redisClient.GetDatabase(defaultDatabase).HashSetAsync(ConvertKey(key), field, val.ToJson());
        }

        public async Task SetAsync<T>(string key, T val, TimeSpan? expire)
        {
            await redisClient.GetDatabase(defaultDatabase).StringSetAsync(ConvertKey(key), val?.ToJson(), expire);
        }
    }
}