namespace FastFrame.Entity
{
    /// <summary>
    /// 标记使用多租户
    /// </summary>
    public interface IHasTenant
    {
        /// <summary>
        /// 租户
        /// </summary>
        string Tenant_Id { get; set; }
    }
}
