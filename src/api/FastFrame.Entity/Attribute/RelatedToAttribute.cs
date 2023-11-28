using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 关联标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RelatedToAttribute(Type relatedType) : Attribute
    {

        /// <summary>
        /// 被关联的类型
        /// </summary>
        public Type RelatedType { get; } = relatedType;
    }
}
