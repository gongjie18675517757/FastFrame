using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 类型管理
    /// </summary>
    public static class TypeManger
    {
        private static readonly Dictionary<string, Type> typeDic = new Dictionary<string, Type>();


        /// <summary>
        /// 所有已注册的类型
        /// </summary>
        public static IEnumerable<Type> RegisterdTypes=> typeDic.Values;


        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterType<T>()
        {
            RegisterType(typeof(T));
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterType(Type type)
        {
            typeDic.TryAdd(type.Name.ToLower(), type);
        }

        /// <summary>
        /// 注册根类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterBaseType<T>()
        {
            RegisterBaseType(typeof(T), typeof(T).Assembly);
        }

        /// <summary>
        /// 注册根类型
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterBaseType(Type type)
        {
            RegisterBaseType(type, type.Assembly);
        }

        /// <summary>
        /// 注册根类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        public static void RegisterBaseType<T>(Assembly assembly)
        {
            RegisterBaseType(typeof(T), assembly);
        }

        /// <summary>
        /// 注册根类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assembly"></param>
        public static void RegisterBaseType(Type type, Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var x in types)
            {
                if (type.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)
                {
                    var name = x.Name.ToLower();
                    if (!typeDic.ContainsKey(name))
                        typeDic.Add(name, x);
                }
            }
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TryGetType(string typeName, out Type type)
        {
            return typeDic.TryGetValue(typeName.ToLower(), out type);
        }
    }
}
