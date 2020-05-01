using FastFrame.Entity.Enums;

namespace FastFrame.Entity
{
    /// <summary>
    /// 标识需要审核
    /// </summary>
    public interface IHaveCheck : IEntity
    {
        /// <summary>
        /// 流程状态
        /// </summary>
        FlowStatusEnum FlowStatus { get; set; }
    }
}
