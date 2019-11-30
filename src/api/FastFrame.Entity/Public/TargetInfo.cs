namespace FastFrame.Entity
{
    /// <summary>
    /// 目标信息
    /// </summary>
    public abstract class TargetInfo : IEntity
    {
        /// <summary>
        /// 接收人
        /// </summary>
        public string To_Id { get; set; }

        /// <summary>
        /// 已读
        /// </summary>
        public bool HaveRead { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
    }
}
