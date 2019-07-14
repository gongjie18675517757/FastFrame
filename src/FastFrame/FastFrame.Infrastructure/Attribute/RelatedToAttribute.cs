using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 关联标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RelatedToAttribute : Attribute
    {
        public RelatedToAttribute(Type relatedType)
        {
            RelatedType = relatedType;
        }

        public RelatedToAttribute(Type relatedType, bool fullProps)
        {
            RelatedType = relatedType;
            FullProps = fullProps;
        }

        /// <summary>
        /// 被关联的类型
        /// </summary>
        public Type RelatedType { get; }

        /// <summary>
        /// 是否全属性
        /// </summary>
        public bool FullProps { get; }
    }
}
