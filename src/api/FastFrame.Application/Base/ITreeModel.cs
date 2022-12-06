using FastFrame.Entity;

namespace FastFrame.Application
{
    /// <summary>
    /// 树模型
    /// </summary>
    public interface ITreeModel : IDto, IViewModel
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        string Super_Id { get; }

        /// <summary>
        /// 直接下级数量
        /// </summary>
        int ChildCount { get; set; }

        /// <summary>
        /// 所有下级数量
        /// </summary>
        int TotalChildCount { get; set; }
    }

    /// <summary>
    /// 默认树
    /// </summary>
    public class TreeModel : ITreeModel, IViewModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string Super_Id { get; set; }

        /// <summary>
        /// 直接下级数量
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// 所有下级数量
        /// </summary>
        public int TotalChildCount { get; set; }

        /// <summary>
        /// 树的文本
        /// </summary>
        public string Value { get; set; }
    }
}
