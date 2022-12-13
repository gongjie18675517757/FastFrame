namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 流程条件比较方式
    /// </summary>
    public enum FlowNodeCondCompareEnum
    {
        /// <summary>
        /// 大于等于
        /// </summary>
        gte = 0,

        /// <summary>
        /// 小于等于
        /// </summary>
        lte = 10,

        /// <summary>
        /// 等于
        /// </summary>
        eq = 20,

        /// <summary>
        /// 不等于
        /// </summary>
        not_eq = 30,

        /// <summary>
        /// 包含
        /// </summary>
        like = 40,

        /// <summary>
        /// 不包含
        /// </summary>
        not_like = 50
    }
}
