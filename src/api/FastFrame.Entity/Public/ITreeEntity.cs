namespace FastFrame.Entity
{
    /// <summary>
    /// 树结构表
    /// </summary>
    public interface ITreeEntity:IEntity
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        string Super_Id { get; } 
    }
}
