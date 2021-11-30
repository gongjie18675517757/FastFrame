namespace FastFrame.Entity.Enums
{
    /// <summary>
    /// 流程状态
    /// </summary>
    public enum FlowStatusEnum
    {
        /// <summary>
        /// 等提交
        /// </summary>
        unsubmitted,

        /// <summary>
        /// 审核中
        /// </summary>
        processing,

        /// <summary>
        /// 审核通过
        /// </summary>
        pass,

        /// <summary>
        /// 审核不通过
        /// </summary>
        ng,
    }
}
