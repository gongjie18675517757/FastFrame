using System;

namespace FastFrame.Entity
{
    /// <summary>
    /// 表单分组
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="groupNames">归属分组名称</param>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class FormGroupAttribute(params string[] groupNames) : Attribute
    {
        public string[] GroupNames { get; } = groupNames;
    }
}
