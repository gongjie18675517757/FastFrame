using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Info
{
    public abstract class BaseMethodInfo
    {
        /// <summary>
        /// 修饰符
        /// </summary>
        public string Modifier { get; set; } = "public";

        /// <summary>
        /// 参数列表
        /// </summary>
        public IEnumerable<ParameterInfo> Parms { get; set; } = new ParameterInfo[] { };

        /// <summary>
        /// 特性
        /// </summary>
        public IEnumerable<AttrInfo> AttrInfos { get; set; } = new AttrInfo[] { };

        /// <summary>
        /// 代码块
        /// </summary>
        public IEnumerable<string> CodeBlock { get; set; } = new string[] { };
    }
}
