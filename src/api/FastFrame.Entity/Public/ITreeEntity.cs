using System.Linq.Expressions;

namespace FastFrame.Entity
{
    /// <summary>
    /// 树结构表
    /// </summary>
    public interface ITreeEntity : IEntity
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        string Super_Id { get; }

        /// <summary>
        /// 树装码
        /// ps:此码是会变的,不可用于关联存储
        /// </summary>
        string TreeCode { get; }
    }

    public interface ITreeEntity<TEntity> : IViewModelable<TEntity>
    {

    }
}
