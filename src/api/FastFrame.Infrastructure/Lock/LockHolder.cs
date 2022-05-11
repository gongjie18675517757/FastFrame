using FastFrame.Infrastructure.Lock;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Lock
{
    public class LockHolder : ILockHolder, System.IDisposable
    {
        private readonly string key;
        private readonly string token;
        private readonly StackExchange.Redis.IDatabase database;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly Task<Task> delayTask;

        public LockHolder(string key, string token, StackExchange.Redis.IDatabase database)
        {
            this.key = key;
            this.token = token;
            this.database = database;
            cancellationTokenSource = new System.Threading.CancellationTokenSource();
            delayTask = Task.Factory.StartNew(delay);
        }

        private async Task delay()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                /*过期了,就跳出*/
                var liveTime = await database.KeyTimeToLiveAsync(key);
                if (liveTime == null)
                {
                    cancellationTokenSource.Cancel();
                    return;
                }

                /*小于2秒时,再延长2秒,直到被取消*/
                if (liveTime.Value.TotalSeconds < 2)
                {
                    Console.WriteLine($"资源:{key}还余:{liveTime}秒,执行延时1秒");
                    //await database.KeyExpireAsync(key, liveTime.Value.Add(TimeSpan.FromSeconds(1)));
                }

                await Task.Delay(1000);
            }
        }

        public async Task LockRelease()
        {
            cancellationTokenSource.Cancel();
            await database.LockReleaseAsync(key, token);
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            GC.SuppressFinalize(this);
        }
    }
}