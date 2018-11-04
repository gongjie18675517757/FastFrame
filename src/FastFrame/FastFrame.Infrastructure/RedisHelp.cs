using StackExchange.Redis;

namespace FastFrame.Infrastructure
{
    public class RedisHelp
    {
        private ConnectionMultiplexer redis { get; set; }
        private IDatabase db { get; set; }
        public RedisHelp(string connection)
        {
            redis = ConnectionMultiplexer.Connect(connection);
            db = redis.GetDatabase();
        } 
        
    }
}
