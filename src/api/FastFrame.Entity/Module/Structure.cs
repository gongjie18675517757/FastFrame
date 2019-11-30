using FastFrame.Infrastructure.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFrame.Entity.Module
{
    /// <summary>
    /// 结构
    /// </summary>
    public class Structure : IEntity, IHasTenant, IHasSoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(150)]
        [Required]
        [ReadOnly(ReadOnlyMark.All)]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 树状键
        /// </summary>
        [ReadOnly(ReadOnlyMark.All)]
        public string TreeKey_Id { get;  set; }

        /// <summary>
        /// 拥有管理属性
        /// </summary>
        [ReadOnly(ReadOnlyMark.All)]
        public bool HasManage { get;  set; }

        public string Id { get; set; }
        public string Tenant_Id { get; set; }
        public bool IsDeleted { get; set; }
    }

    
}
