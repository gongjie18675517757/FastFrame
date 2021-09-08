namespace FastFrame.Infrastructure.Identity
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public interface ICurrUser
    {
        /// <summary>
        /// 当前ToKen的ID
        /// </summary>
        string ToKen { get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 帐号名
        /// </summary>
        string Account { get; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        bool IsAdmin { get; set; }
    }

    /// <summary>
    /// 数据权限
    /// </summary>
    public enum DataPermission
    {
        /// <summary>
        /// 本人录入
        /// </summary>
        A,

        /// <summary>
        /// 本部门录入
        /// </summary>
        B,

        /// <summary>
        /// 全部
        /// </summary>
        C
    }
}
