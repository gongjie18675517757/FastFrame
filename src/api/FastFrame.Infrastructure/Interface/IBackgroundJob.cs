using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 后台执行任务
    /// </summary>
    public interface IBackgroundJob
    {
        /// <summary>
        /// 定时执行 
        /// <para>* * * * * *</para>
        /// <para>- - - - - -</para>
        /// <para>| | | | | |</para>
        /// <para>| | | | | +--- day of week (0 - 6) (Sunday=0)</para>
        /// <para>| | | | +----- month (1 - 12)</para>
        /// <para>| | | +------- day of month (1 - 31)</para>
        /// <para>| | +--------- hour (0 - 23)</para>
        /// <para>| +----------- min (0 - 59)</para>
        /// <para>+------------- sec (0 - 59)</para>
        /// </summary> 
        /// <typeparam name="TService"></typeparam>
        /// <param name="methodCall"></param>
        /// <param name="cronExperssion"></param>
        void SetInterval<TService>(Expression<Func<TService, Task>> methodCall, string cronExperssion);

        /// <summary>
        /// 定时执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="cron"></param>
        void SetIntervalByMethod<T>(MethodInfo method, string cron)
        {
            var type = typeof(T);
            var parameterExpression = Expression.Parameter(type, "x");
            var methodCallExpression = Expression.Call(parameterExpression, method);
            var expression = Expression.Lambda<Func<T, Task>>(methodCallExpression, parameterExpression);

            SetInterval(expression, cron);
        }




        /// <summary>
        /// 延时执行
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="methodCall"></param>
        /// <param name="timeSpan"></param>
        void SetTimeout<TService>(Expression<Func<TService, Task>> methodCall, TimeSpan? timeSpan);

        /// <summary>
        /// 延时执行
        /// </summary>
        /// <typeparam name="TService"></typeparam> 
        /// <param name="method"></param>
        /// <param name="parms"></param>
        /// <param name="timeSpan"></param>
        void SetTimeoutByMethod<TService>(MethodInfo method, object[] parms, TimeSpan? timeSpan)
        {
            var parameterExpression = Expression.Parameter(typeof(TService), "x");
            var arguments = method.GetParameters().Zip(parms, (a, b) => Expression.Constant(b, a.ParameterType)).ToArray();
            var methodCallExpression = Expression.Call(parameterExpression, method, arguments);
            var expression = Expression.Lambda<Func<TService, Task>>(methodCallExpression, parameterExpression);
            SetTimeout(expression, timeSpan);
        }  

        /// <summary>
        /// 每分钟
        /// </summary>
        public static string Minutely()
        {
            return "* * * * *";
        }

        /// <summary>
        /// 每分钟
        /// </summary>
        public static string Minutely(int second)
        {
            return $"{second} * * * * *";
        }

        /// <summary>
        /// 每小时
        /// </summary>
        public static string Hourly()
        {
            return Hourly(minute: 0);
        }

        /// <summary>
        /// 每小时
        /// </summary> 
        public static string Hourly(int minute)
        {
            return $"{minute} * * * *";
        }

        /// <summary>
        /// 每天
        /// </summary>
        public static string Daily()
        {
            return Daily(hour: 0);
        }

        /// <summary>
        /// 每天
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static string Daily(int hour)
        {
            return Daily(hour, minute: 0);
        }

        /// <summary>
        /// 每天
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static string Daily(int hour, int minute)
        {
            return $"{minute} {hour} * * *";
        }

        /// <summary>
        /// 每周
        /// </summary>
        /// <returns></returns>
        public static string Weekly()
        {
            return Weekly(DayOfWeek.Monday);
        }

        /// <summary>
        /// 每周
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string Weekly(DayOfWeek dayOfWeek)
        {
            return Weekly(dayOfWeek, hour: 0);
        }


        /// <summary>
        /// 每周
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static string Weekly(DayOfWeek dayOfWeek, int hour)
        {
            return Weekly(dayOfWeek, hour, minute: 0);
        }

        /// <summary>
        /// 每周
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static string Weekly(DayOfWeek dayOfWeek, int hour, int minute)
        {
            return $"{minute} {hour} * * {(int)dayOfWeek}";
        }

        /// <summary>
        /// 每月
        /// </summary>
        /// <returns></returns>
        public static string Monthly()
        {
            return Monthly(day: 1);
        }

        /// <summary>
        /// 每月
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string Monthly(int day)
        {
            return Monthly(day, hour: 0);
        }

        /// <summary>
        /// 每月
        /// </summary>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static string Monthly(int day, int hour)
        {
            return Monthly(day, hour, minute: 0);
        }

        /// <summary>
        /// 每月
        /// </summary>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static string Monthly(int day, int hour, int minute)
        {
            return $"{minute} {hour} {day} * *";
        }

        /// <summary>
        /// 每年
        /// </summary>
        /// <returns></returns>
        public static string Yearly()
        {
            return Yearly(month: 1);
        }

        /// <summary>
        /// 每年
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string Yearly(int month)
        {
            return Yearly(month, day: 1);
        }

        /// <summary>
        /// 每年
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string Yearly(int month, int day)
        {
            return Yearly(month, day, hour: 0);
        }

        /// <summary>
        /// 每年
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static string Yearly(int month, int day, int hour)
        {
            return Yearly(month, day, hour, minute: 0);
        }

        /// <summary>
        /// 每年
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static string Yearly(int month, int day, int hour, int minute)
        {
            return $"{minute} {hour} {day} {month} *";
        }

        /// <summary>
        /// 从不执行
        /// </summary>
        /// <returns></returns>
        public static string Never()
        {
            return Yearly(2, 31);
        }
    }
}
