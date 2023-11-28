using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Entity
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="uniqueNames">需要验证唯一的属性</param>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class UniqueAttribute(params string[] uniqueNames) : Attribute
    {
        public string[] UniqueNames { get; } = uniqueNames;
    }
}
