using FastFrame.Entity;

namespace FastFrame.Application
{
    /// <summary>
    /// 树模型
    /// </summary>
    public interface ITreeModel : IDto
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

    public class TreeModel : ITreeModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string Super_Id { get; set; }

        /// <summary>
        /// 下级数量
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 树状码
        /// </summary>
        public string TreeCode { get; set; }
    }
}
