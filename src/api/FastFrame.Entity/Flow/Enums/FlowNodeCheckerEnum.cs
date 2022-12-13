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
        user = 0,

        /// <summary>
        /// 指定角色
        /// </summary>
        role = 10,

        /// <summary>
        /// 指定字段
        /// </summary>
        field = 20,

        /// <summary>
        /// 指定部门人员
        /// </summary>
        dept = 30,

        /// <summary>
        /// 指定部门主管
        /// </summary>
        dept_manage,

        /// <summary>
        /// 上级部门人员
        /// </summary>
        parent_dept = 40,

        /// <summary>
        /// 上级部门主管
        /// </summary>
        parent_dept_manage = 50,

        /// <summary>
        /// 当前部门主管
        /// </summary>
        cur_dept_manage = 60,

        /// <summary>
        /// 上个节点指定
        /// </summary>
        prev_appoint = 70
    }
}
