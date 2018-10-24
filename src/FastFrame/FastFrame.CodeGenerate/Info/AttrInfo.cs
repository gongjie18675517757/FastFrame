using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Info
{
    /// <summary>
    /// 特性信息
    /// </summary>
    public class AttrInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public IEnumerable<string> Parameters { get; set; } = new string[] { };
    }
}
