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
        start = 0,

        /// <summary>
        /// 分支
        /// </summary>
        branch = 10,

        /// <summary>
        /// 子分支
        /// </summary>
        branch_child = 20,

        /// <summary>
        /// 审核 
        /// </summary>
        check = 30,

        /// <summary>
        /// 抄送
        /// </summary>
        cc = 40,

        /// <summary>
        /// 条件
        /// </summary>
        cond = 50,

        /// <summary>
        /// 结束
        /// </summary>
        end = 60
    }
}
