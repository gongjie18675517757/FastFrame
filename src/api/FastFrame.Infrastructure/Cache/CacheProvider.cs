using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
using FastFrame.Infrastructure.Lock;
using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Cache
{

    public class CacheProvider : ICacheProvider 
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


        public static T ConvertValue<T>(StackExchange.Redis.RedisValue value)
        {
            if (!value.HasValue)
                return default;

            return value.ToString().ToObject<T>();
        }

        public static string ConvertKey(string key,string prefix)
        {
            if (prefix.IsNullOrWhiteSpace())
                return key;

            return $"{prefix}{key}";
        }

        private string ConvertKey(string key)
        {
            return ConvertKey(key, prefix);
        }

       

        private StackExchange.Redis.IDatabase GetDatabase()
        {
            return redisClient.GetDatabase(defaultDatabase);
        }

        public Task DelAsync(string key)
        {

            return GetDatabase().KeyDeleteAsync(ConvertKey(key));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await GetDatabase().StringGetAsync(ConvertKey(key));
            return ConvertValue<T>(value);
        }

        public async Task<T> HGetAsync<T>(string key, string field)
        {
            try
            {
                var value = await GetDatabase().HashGetAsync(ConvertKey(key), field);
                return ConvertValue<T>(value);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task HSetAsync<T>(string key, string field, T val, TimeSpan? expire)
        {
            await GetDatabase().HashSetAsync(ConvertKey(key), field, val.ToJson());
        }

        public async Task SetAsync<T>(string key, T val, TimeSpan? expire)
        {
            await GetDatabase().StringSetAsync(ConvertKey(key), val?.ToJson(), expire);
        }
    }
}