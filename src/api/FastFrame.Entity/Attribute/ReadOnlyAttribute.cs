using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 标记只读
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ReadOnlyAttribute(ReadOnlyMark readOnlyMark = ReadOnlyMark.All) : Attribute
    {
        /// <summary>
        /// 只读类型
        /// </summary>
        public ReadOnlyMark ReadOnlyMark { get; } = readOnlyMark;
    }
}
