using System;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 数据重复异常
    /// </summary>
    public class UniqueException : Exception
    {
        public UniqueException(Type type,string[] propNames)
        {
            Type = type;
            PropNames = propNames;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 重复的字段名称
        /// </summary>
        public string[] PropNames { get; }
    }
}
