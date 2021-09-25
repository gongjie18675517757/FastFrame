namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程节点条件
    /// </summary>
    public class FlowNodeCond : IEntity, IHasSoftDelete
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
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 条件组号
        /// </summary>
        public int GroupIndex { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public FlowNodeCondCompareEnum CompareEnum { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public FlowNodeCondValueEnum ValueEnum { get; set; }

        /// <summary>
        /// 值的ID
        /// </summary>
        public string Value_Id { get; set; }

        /// <summary>
        /// 值的文本
        /// </summary>
        public string ValueText { get; set; }
    }
}
