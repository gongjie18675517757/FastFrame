namespace FastFrame.Entity.Enums
{
    /// <summary>
    /// 流程状态
    /// </summary>
    public enum FlowStatusEnum
    {
        /// <summary>
        /// 未提交
        /// </summary>
        unsubmitted,

        /// <summary>
        /// 已提交
        /// </summary>
        submitted,

        /// <summary>
        /// 进行中
        /// </summary>
        processing,

        /// <summary>
        /// 已完结
        /// </summary>
        finished,

        /// <summary>
        /// 已退回
        /// </summary>
        returned,

        /// <summary>
        /// 已拒绝
        /// </summary>
        refuse
    }
}
