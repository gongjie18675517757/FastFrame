﻿using FastFrame.Infrastructure.Interface;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.IntervalWork
{
    public class IntervalWorkServiceInit :  IApplicationInitialLifetime
    {
        private readonly IBackgroundJob backgroundJob;

        public IntervalWorkServiceInit(IBackgroundJob backgroundJob)
        {
            this.backgroundJob = backgroundJob;
        }

        public async Task InitialAsync()
        {
            await Task.CompletedTask;
            var methodList = IntervalWorkServiceCollectionExtensions.IntervalMethodList;
            if (methodList.Count > 0)
            {
                var method = typeof(IBackgroundJob)
                    .GetMethod(nameof(IBackgroundJob.SetIntervalByMethod), BindingFlags.Instance | BindingFlags.Public);

                foreach (var item in methodList)
                {
                    method.MakeGenericMethod(item.type).Invoke(backgroundJob, new object[] {
                        item.job_id,
                        item.method,
                        item.cron
                    });
                }
            }
        }
    }
}
