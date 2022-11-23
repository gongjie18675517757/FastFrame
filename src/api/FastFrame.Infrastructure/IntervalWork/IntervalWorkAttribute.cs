//using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.IntervalWork
{
    /// <summary>
    /// 后台定时任务
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class IntervalWorkAttribute : /*AbstractInterceptor*/Attribute
    {
        public IntervalWorkAttribute(string jobId, string cronExperssion)
        {
            CronExperssion = cronExperssion;
            JobId = jobId;
        }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExperssion { get; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public string JobId { get; }
    }
}
