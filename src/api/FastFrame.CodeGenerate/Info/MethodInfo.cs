namespace FastFrame.CodeGenerate.Info
{
    /// <summary>
    /// 方法信息
    /// </summary>
    public class MethodInfo : BaseMethodInfo
    { 
        /// <summary>
        /// 是否重写父类方法
        /// </summary>
        public bool IsOverride { get; set; }

        /// <summary>
        /// 返回值类型
        /// </summary>
        public string ResultTypeName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }
    }
}
