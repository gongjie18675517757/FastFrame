using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 关联标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RelatedToAttribute : Attribute
    {
        public RelatedToAttribute(Type relatedType, string relatedKey = "Id")
        {
            RelatedType = relatedType;
            RelatedKey = relatedKey;
        }

        /// <summary>
        /// 被关联的类型
        /// </summary>
        public Type RelatedType { get; }
        public string RelatedKey { get; }
    }

    /// <summary>
    /// 表单分组
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class FormGroupAttribute : Attribute
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupNames">归属分组名称</param>
        public FormGroupAttribute(params string[] groupNames)
        {
            this.GroupNames = groupNames;
        }

        public string[] GroupNames { get; }
    }
}
