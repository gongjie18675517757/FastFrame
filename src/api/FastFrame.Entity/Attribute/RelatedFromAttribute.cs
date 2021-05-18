using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 关联自
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RelatedFromAttribute : Attribute
    {
        public RelatedFromAttribute(string fromPropName,string relatedFromPropName, bool isDefault=false)
        {
            FromPropName = fromPropName;
            RelatedFromPropName = relatedFromPropName;
            IsDefault = isDefault;
        }

        /// <summary>
        /// 来源属性名
        /// </summary>
        public string FromPropName { get; }

        /// <summary>
        /// 关联属性名
        /// </summary>
        public string RelatedFromPropName { get; }

        /// <summary>
        /// 是否默认字段
        /// </summary>
        public bool IsDefault { get; }
    }
}
