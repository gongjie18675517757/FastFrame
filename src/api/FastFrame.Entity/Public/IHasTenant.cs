namespace FastFrame.Entity
{
    /// <summary>
    /// 标记使用多租户
    /// </summary>
    public interface IHasTenant
    { 
        /// <summary>
        /// 租户ID,根租户为null
        /// </summary>
        string Tenant_Id { get; set; }
    }
}
