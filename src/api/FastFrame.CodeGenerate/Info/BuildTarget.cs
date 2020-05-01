namespace FastFrame.CodeGenerate.Info
{
    /// <summary>
    /// 生成目标
    /// </summary>
    public class BuildTarget
    {
        /// <summary>
        /// 目标全路径
        /// </summary>
        public string TargetPath { get; set; }

        /// <summary>
        /// 是否强制覆盖
        /// </summary>
        public bool Forcibly { get; set; } = false;

        /// <summary>
        /// 代码块
        /// </summary>
        public string CodeBlock { get; set; }
    }
}
