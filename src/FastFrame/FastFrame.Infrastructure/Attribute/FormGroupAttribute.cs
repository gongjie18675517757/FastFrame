using System;

namespace FastFrame.Infrastructure.Attrs
{
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
