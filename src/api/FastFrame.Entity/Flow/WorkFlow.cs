using FastFrame.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 工作流    
    /// </summary>
    [Export] 
    public class WorkFlow : BaseEntity
    {
        /// <summary>
        /// 适用模块
        /// </summary>
        [StringLength(100)]
        [ReadOnly(ReadOnlyMark.Edit)]
        public string BeModule { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [ReadOnly]
        [StringLength(150)]
        [IsPrimaryField]
        public string BeModuleName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [ReadOnly]
        public int Version { get; set; } = 1;

        /// <summary>
        /// 状态
        /// </summary>
        [ReadOnly]
        [EnumItem(EnumName.EnabledMark)]
        public int Enabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remarks { get; set; }
    }
}
