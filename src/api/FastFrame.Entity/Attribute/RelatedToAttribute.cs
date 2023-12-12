using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 关联标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public abstract class RelatedToAttribute : Attribute
    {
        /// <summary>
        /// 被关联的类型
        /// </summary>
        public abstract Type RelatedType { get; }
    }


    /// <summary>
    /// 关联标记
    /// </summary> 
    public sealed class RelatedToAttribute<TEntity> : RelatedToAttribute
    {
        public override Type RelatedType => typeof(TEntity);
    }

    /// <summary>
    /// 标记属性为关联出来的值
    /// </summary> 
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ValueRelateFor(string fieldName, Type relatedType) : Attribute
    {
        /// <summary>
        /// 类型字段
        /// </summary>
        public string FieldName { get; } = fieldName ?? throw new ArgumentNullException(nameof(fieldName));

        /// <summary>
        /// 关联类型
        /// </summary>
        public Type RelatedType { get; set; } = relatedType ?? throw new ArgumentNullException(nameof(relatedType));
    }
}
