using System;
using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Info
{
    /// <summary>
    /// 构造函数信息
    /// </summary>
    public class ConstructorInfo : BaseMethodInfo
    {
        /// <summary>
        /// 传递给父类的构造函数
        /// </summary>
        public IEnumerable<string> Super { get; set; } = Array.Empty<string>();
    }
}
