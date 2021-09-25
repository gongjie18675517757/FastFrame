namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum FlowNodeEnum
    {
        /// <summary>
        /// 开始
        /// </summary>
        start,

        /// <summary>
        /// 分支
        /// </summary>
        branch,

        /// <summary>
        /// 子分支
        /// </summary>
        branch_child,

        /// <summary>
        /// 审核 
        /// </summary>
        check,

        /// <summary>
        /// 抄送
        /// </summary>
        cc,

        /// <summary>
        /// 条件
        /// </summary>
        cond,

        /// <summary>
        /// 结束
        /// </summary>
        end
    }
}
