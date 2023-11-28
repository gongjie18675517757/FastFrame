using FastFrame.Entity.Enums;
using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class EnumItemAttribute(EnumName name, string superPropName = null) : Attribute
    {

        /// <summary>
        /// 字典值
        /// </summary>
        public EnumName Name { get; } = name;

        /// <summary>
        /// 上级
        /// </summary>
        public string SuperPropName { get; } = superPropName;
    }
}
