namespace FastFrame.Entity
{
    /// <summary>
    /// 树结构表
    /// </summary>
    public interface ITreeEntity
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        string Super_Id { get; }

        /// <summary>
        /// 下级数量
        /// </summary>
        int ChildCount { get; set; }
    }
}
