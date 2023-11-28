using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastFrame.Infrastructure.MessageQueue
{
    public static class MessageQueueServiceCollectionExtensions
    {
        private static readonly Dictionary<string, List<(Type type, MethodInfo method)>> dic = [];

        /// <summary>
        /// 订阅列表
        /// </summary>
        public static IReadOnlyDictionary<string, List<(Type type, MethodInfo method)>> SubscribeMethodList => dic;

        /// <summary>
        /// 添加消息队列
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMessageQueue(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
                AddAssembly(assembly);

            return services;
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="assembly"></param>
        private static void AddAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
                if (typeof(IMessageSubscribeHost).IsAssignableFrom(type))
                    foreach (var method in type.GetMethods())
                        if (method.GetCustomAttribute<MessageSubscribeAttribute>() is MessageSubscribeAttribute attribute)
                        {
                            var list = dic.TryGetValueOrCreate(attribute.Channel, func: CreateNewItem);

                            list.Add((type, method));
                        }
        }

        private static List<(Type type, MethodInfo method)> CreateNewItem()
        {
            return [];
        }
    }
}
