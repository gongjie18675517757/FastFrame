namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程动作
    /// </summary>
    public enum FlowActionEnum
    {
        /// <summary>
        /// 提交
        /// </summary>
        submit = 0,

        /// <summary>
        /// 撤回
        /// </summary>
        unsubmit = 10,

        /// <summary>
        /// 同意
        /// </summary>
        pass = 20,

        /// <summary>
        /// 不同意
        /// </summary>
        ng = 30,

        /// <summary>
        /// 反审核
        /// </summary>
        uncheck = 40,
    }
}
