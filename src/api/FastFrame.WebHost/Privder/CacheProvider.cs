using CSRedis;
using FastFrame.Infrastructure.Interface;
using System;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class CacheProvider : ICacheProvider
    {
        private readonly CSRedisClient redisClient;

        public CacheProvider(CSRedisClient redisClient)
        {
            this.redisClient = redisClient;
        }
        public Task DelAsync(string key)
        {
            return redisClient.DelAsync(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return redisClient.GetAsync<T>(key);
        }

        public Task<T> HGetAsync<T>(string key, string field)
        {
            return redisClient.HGetAsync<T>(key, field);
        }

        public Task HSetAsync<T>(string key, string field, T val, TimeSpan? expire)
        {
            return redisClient.HSetAsync(key, field, val);
        }

        public Task SetAsync<T>(string key, T val, TimeSpan? expire)
        {
            var expireSeconds = -1;
            if (expire != null)
                expireSeconds = Convert.ToInt32(expire.Value.TotalSeconds);
            return redisClient.SetAsync(key, val, expireSeconds);
        }
    }
}