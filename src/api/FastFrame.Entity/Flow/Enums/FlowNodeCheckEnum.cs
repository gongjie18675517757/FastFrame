namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 审批方式
    /// </summary>
    public enum FlowNodeCheckEnum
    {
        /// <summary>
        /// 或签
        /// </summary>
        or = 0,

        /// <summary>
        /// 会签
        /// </summary>
        and = 10,

        /// <summary>
        /// 投票
        /// </summary>
        vote=20,
    }
}
