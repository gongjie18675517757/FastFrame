using System;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 数据重复异常
    /// </summary>
    public class UniqueException(Type type, string[] propNames) : Exception
    {

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; } = type;

        /// <summary>
        /// 重复的字段名称
        /// </summary>
        public string[] PropNames { get; } = propNames;
    }
}
