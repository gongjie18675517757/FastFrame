using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 标记只读
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ReadOnlyAttribute : Attribute
    {
        public ReadOnlyAttribute(ReadOnlyMark readOnlyMark = ReadOnlyMark.All)
        {
            ReadOnlyMark = readOnlyMark;
        }
        /// <summary>
        /// 只读类型
        /// </summary>
        public ReadOnlyMark ReadOnlyMark { get; }
    }
}
