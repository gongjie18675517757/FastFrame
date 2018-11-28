namespace FastFrame.Entity
{
    /// <summary>
    /// 使用软删除
    /// </summary>
    public interface IHasSoftDelete
    {
        /// <summary>
        /// 删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
