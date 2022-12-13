namespace FastFrame.Entity.Enums
{
    /// <summary>
    /// 流程状态
    /// </summary>
    public enum FlowStatusEnum
    {
        /// <summary>
        /// 待提交
        /// </summary>
        unsubmitted = 0,

        /// <summary>
        /// 审核中
        /// </summary>
        processing = 10,

        /// <summary>
        /// 审核通过
        /// </summary>
        pass = 20,

        /// <summary>
        /// 审核不通过
        /// </summary>
        ng = 30,
    }
}
