using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFrame.Dto.Module
{
    public class RuleOutPut
    {
        public string Field_Id { get; set; }

        public string Id { get; set; }
        /// <summary>
        /// 规则名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string RuleName { get; }

        /// <summary>
        /// 规则参数
        /// </summary>
        public IEnumerable<string> RulePars { get; set; }
    }
}
