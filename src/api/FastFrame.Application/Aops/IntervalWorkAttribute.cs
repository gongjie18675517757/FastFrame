using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 后台定时任务
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class IntervalWorkAttribute : AbstractInterceptorAttribute
    {
        public IntervalWorkAttribute(string cronExperssion)
        {
            CronExperssion = cronExperssion;
        }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExperssion { get; }



        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            /*此处判断方法是否在执行中*/ 
            await next(context);
        }
    } 
}
