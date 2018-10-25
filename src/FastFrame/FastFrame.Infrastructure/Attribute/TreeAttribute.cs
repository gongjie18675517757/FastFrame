using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 标记是树形结构
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TreeAttribute : Attribute
    {
        public TreeAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        /// 关键属性
        /// </summary>
        public string Key { get; }
    }
}
