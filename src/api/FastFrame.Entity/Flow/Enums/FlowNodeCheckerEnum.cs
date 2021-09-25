namespace FastFrame.Entity.Flow
{
    /// <summary>
    /// 审核人枚举
    /// </summary>
    public enum FlowNodeCheckerEnum
    {
        /// <summary>
        /// 指定人
        /// </summary>
        user,

        /// <summary>
        /// 指定角色
        /// </summary>
        role,

        /// <summary>
        /// 指定字段
        /// </summary>
        field,

        /// <summary>
        /// 指定部门人员
        /// </summary>
        dept,

        /// <summary>
        /// 上级部门人员
        /// </summary>
        parent_dept,

        /// <summary>
        /// 上级部门主管
        /// </summary>
        parent_dept_manage,

        /// <summary>
        /// 当前部门主管
        /// </summary>
        dept_manage,

        /// <summary>
        /// 上个节点指定
        /// </summary>
        prev_appoint
    }
}
