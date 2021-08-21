using FastFrame.Infrastructure.Interface;
using Hangfire;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class HangfireBackgroundJob : IBackgroundJob
    {
        private readonly IServiceProvider loader;

        public HangfireBackgroundJob(IServiceProvider loader)
        {
            this.loader = loader; 
        }

        public void SetInterval<TService>(Expression<Func<TService, Task>> methodCall, string cronExperssion)
        {
            RecurringJob.AddOrUpdate<TService>(methodCall, Cron.Minutely());
        }

        public void SetTimeout<TService>(Expression<Func<TService, Task>> methodCall, TimeSpan? timeSpan)
        {
            if (timeSpan == null)
                BackgroundJob.Enqueue(methodCall);
            else
                BackgroundJob.Schedule(methodCall, delay: timeSpan.Value);
        }

        public string SerializePayloadExpression<TService>(Expression<Func<TService, Task>> methodCall)
        {
            var job = Hangfire.Common.Job.FromExpression(methodCall);
            var invocationData = Hangfire.Storage.InvocationData.SerializeJob(job);
            var payload = invocationData.SerializePayload(excludeArguments: false);

            return payload;
        }

        public async Task TryExecJob(string payload)
        {
            //var cacheProvider = loader.GetService<ICacheProvider>();
            //var memoryCache = loader.GetService<Microsoft.Extensions.Caching.Memory.IMemoryCache>();

            var invocationData = Hangfire.Storage.InvocationData.DeserializePayload(payload);
            var job = invocationData.DeserializeJob();
            if (job == null)
                throw new InvalidOperationException("payload DeserializeJob fail");

            object instance = null;
            if (!job.Method.IsStatic)
            {
                instance = loader.GetService(job.Type);
                if (instance == null)
                {
                    throw new InvalidOperationException(
                             $"JobActivator returned NULL instance of the '{job.Type}' type.");
                }
            }

            var parameters = job.Args.ToArray();
            var result = (Task)job.Method.Invoke(instance, parameters);
            await result;
        }


    }
}