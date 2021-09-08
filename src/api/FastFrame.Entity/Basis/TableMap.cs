namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 表映射
    /// </summary>
    public class TableMap : IEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        public virtual string FKey_Id { get; set; }

        /// <summary>
        /// 键名
        /// </summary>
        public virtual string KeyName { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        public virtual string Value_Id { get; set; }
    }
}
