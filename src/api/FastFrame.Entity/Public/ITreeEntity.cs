namespace FastFrame.Entity
{
    /// <summary>
    /// 树结构表
    /// </summary>
    public interface ITreeEntity : IEntity
    {
        string Super_Id { get; }
    }
}
