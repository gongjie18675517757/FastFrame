using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class UniqueAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueNames">需要验证唯一的属性</param>
        public UniqueAttribute(params string[] uniqueNames)
        {
            UniqueNames = uniqueNames;
        }

        public string[] UniqueNames { get; }
    }
}
