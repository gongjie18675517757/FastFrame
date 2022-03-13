using FastFrame.Entity.Flow;

namespace FastFrame.Application.Flow
{
    public partial class DFModuleDto
    {
        /// <summary>
        /// 分组
        /// </summary>
        public IEnumerable<DFModuleGroupModel> Groups { get; set; }
    }

    /// <summary>
    /// 表单分组
    /// </summary>
    public partial class DFModuleGroupModel : DFModuleGroup, IDto
    {
        /// <summary>
        /// 字段列表
        /// </summary>
        public IEnumerable<DFModuleFieldModel> Fields { get; set; }
    }

    /// <summary>
    /// 分组字段
    /// </summary>
    public partial class DFModuleFieldModel : DFModuleField, IDto
    {
        /// <summary>
        /// 验证规则 
        /// </summary>
        public IEnumerable<DFModuleFieldRule> Rules { get; set; }
    }
}
