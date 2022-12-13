using FastFrame.Entity.Enums;

namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程节点审核人/抄送人
    /// </summary>
    public class FlowNodeChecker : IEntity, IHasSoftDelete
    {
        public string Id { get; set; }

        /// <summary>
        /// 关联:WorkFlow
        /// </summary>
        public string WorkFlow_Id { get; set; }

        /// <summary>
        /// 关联:FlowNode
        /// </summary>
        public string FlowNode_Id { get; set; }

        /// <summary>
        /// 审核人类型
        /// </summary>
        [EnumItem(EnumName.FlowNodeCheckerEnum)]
        public int CheckerEnum { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Checker_Id { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        public string CheckerName { get; set; }
    }
}
