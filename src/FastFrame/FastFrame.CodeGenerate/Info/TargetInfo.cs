using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Info
{
    /// <summary>
    /// 生成目标信息
    /// </summary>
    public class TargetInfo
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NamespaceName { get; set; }

        /// <summary>
        /// 要引用的命名空间
        /// </summary>
        public IEnumerable<string> ImportNames { get; set; } = new string[] { };

        /// <summary>
        /// 目标类型名称:class/interface
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 特性列表
        /// </summary>
        public IEnumerable<AttrInfo> AttrInfos { get; set; } = new AttrInfo[] { };

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父类型
        /// </summary>
        public IEnumerable<string> BaseNames { get; set; } = new string[] { };

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConstructorInfo Constructor { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public IEnumerable<PropInfo> PropInfos { get; set; } = new PropInfo[] { };

        /// <summary>
        /// 方法列表
        /// </summary>
        public IEnumerable<MethodInfo> MethodInfos { get; set; } = new MethodInfo[] { };
    } 
}
