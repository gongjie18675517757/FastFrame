using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 模块结构
    /// </summary>
    public class ModuleStruct
    {
        /// <summary>
        /// 名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary> 
        public string Description { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public List<ModuleFieldStrut> FieldInfoStruts { get; set; }

        /// <summary>
        /// 被关联时显示的字段列表
        /// </summary>
        public IEnumerable<string> RelateFields { get; set; }

        /// <summary>
        /// 树结构
        /// </summary>
        public bool IsTree { get; set; }

        /// <summary>
        /// 有管理属性
        /// </summary>
        public bool HasManage { get; set; }

        /// <summary>
        /// 默认实体
        /// </summary>
        public object Form { get; set; }

        /// <summary>
        /// 有附件
        /// </summary>
        public bool HasFiles { get; set; }

        /// <summary>
        /// 需要流程审核
        /// </summary>
        public bool HaveCheck { get; set; }
    }
}
