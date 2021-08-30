using FastFrame.Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FastFrame.Infrastructure.IntervalWork
{
    public static class IntervalWorkServiceCollectionExtensions
    {
        private static readonly List<(Type type, MethodInfo method, string cron)> list = new List<(Type type, MethodInfo method, string cron)>();

        /// <summary>
        /// 定时任务列表
        /// </summary>
        public static IReadOnlyList<(Type type, MethodInfo method, string cron)> IntervalMethodList => list;

        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddIntervalWork(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
                AddAssembly(assembly);

            services.AddScoped<IApplicationInitialLifetime, IntervalWorkServiceInit>();

            return services;
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="assembly"></param>
        private static void AddAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
                if (typeof(IIntervalWorkHost).IsAssignableFrom(type))
                    foreach (var method in type.GetMethods())
                        if (method.GetCustomAttribute<IntervalWorkAttribute>() is IntervalWorkAttribute attribute)
                            list.Add((type, method, attribute.CronExperssion));
        }
    }
}
