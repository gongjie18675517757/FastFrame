namespace FastFrame.Entity
{
    public abstract class BaseEntity : IEntity, IHasTenant, IHasSoftDelete,IHasManage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        [Infrastructure.Attrs.Exclude]
        public string Tenant_Id { get; set; }

        /// <summary>
        /// 删除码
        /// </summary>
        [Infrastructure.Attrs.Exclude]
        public bool IsDeleted { get; set; }
    }
}
