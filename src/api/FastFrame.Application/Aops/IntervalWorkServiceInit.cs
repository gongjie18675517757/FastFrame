using FastFrame.Infrastructure.Interface;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    public class IntervalWorkServiceInit : IService, IApplicationInitialLifetime
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
                    .GetMethod(nameof(IBackgroundJob.SetInterval), BindingFlags.Instance | BindingFlags.Public);

                foreach (var item in methodList)
                {
                    method.MakeGenericMethod(item.type).Invoke(backgroundJob, new object[] { 
                        item.method,
                        item.cron
                    });
                }
            } 
        }  
    }
}
