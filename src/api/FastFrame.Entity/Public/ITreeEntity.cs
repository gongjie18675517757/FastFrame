namespace FastFrame.Entity
{
    /// <summary>
    /// 树结构表
    /// </summary>
    public interface ITreeEntity : IEntity, IHaveNumber
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        string Super_Id { get; } 

        /// <summary>
        /// 树装码
        /// </summary>
        string TreeCode { get; }
    }
}
