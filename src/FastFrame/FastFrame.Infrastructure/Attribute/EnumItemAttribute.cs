using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class EnumItemAttribute : Attribute
    {
        public EnumItemAttribute(string name, string superPropName = null)
        {
            Name = name;
            SuperPropName = superPropName;
        }

        /// <summary>
        /// 字典值
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 上级
        /// </summary>
        public string SuperPropName { get; }
    }
}
