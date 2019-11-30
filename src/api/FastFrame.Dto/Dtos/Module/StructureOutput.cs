using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Dto.Module
{
    public class StructureOutput
    {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(150)]
        [Required]
        public string Description { get; set; }


        /// <summary>
        /// 拥有管理属性
        /// </summary>
        public bool HasManage { get; set; }

        /// <summary>
        /// 树状键
        /// </summary>
        public string TreeKeyName { get; set; }

        /// <summary>
        /// 被关联时显示的字段列表
        /// </summary>
        public IEnumerable<string> RelateFields { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public IEnumerable<StrucFieldOutput> FieldInfoStruts { get; set; }
    }
}
