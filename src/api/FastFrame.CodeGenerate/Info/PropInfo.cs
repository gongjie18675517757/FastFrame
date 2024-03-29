﻿using System.Collections.Generic;

namespace FastFrame.CodeGenerate.Info
{
    /// <summary>
    /// 属性信息
    /// </summary>
    public class PropInfo
    {
        /// <summary>
        /// 说明 
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 特性列表
        /// </summary>
        public IEnumerable<AttrInfo> AttrInfos { get; set; } = [];

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Get的代码块
        /// </summary>
        public IEnumerable<string> GetCodeBlock { get; set; } = [];

        /// <summary>
        /// Set的代码块
        /// </summary>
        public IEnumerable<string> SetCodeBlock { get; set; } = [];
    }
}
