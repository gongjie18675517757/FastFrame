using FastFrame.Entity.Enums;
using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class EnumItemAttribute : Attribute
    {
        public EnumItemAttribute(EnumName name, string superPropName = null)
        {
            Name = name;
            SuperPropName = superPropName;
        }

        /// <summary>
        /// 字典值
        /// </summary>
        public EnumName Name { get; }

        /// <summary>
        /// 上级
        /// </summary>
        public string SuperPropName { get; }
    }
}
