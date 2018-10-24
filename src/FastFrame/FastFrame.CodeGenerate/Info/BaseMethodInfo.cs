using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Info
{
    public abstract class BaseMethodInfo
    {
        /// <summary>
        /// 参数列表
        /// </summary>
        public IEnumerable<ParameterInfo> Parms { get; set; } = new ParameterInfo[] { };

        /// <summary>
        /// 代码块
        /// </summary>
        public IEnumerable<string> CodeBlock { get; set; } = new string[] { };
    }
}
