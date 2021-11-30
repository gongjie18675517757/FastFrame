using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程操作前(状态还未生成)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class FlowOperateBefore<TEntity> : IEventData where TEntity : IHaveCheck
    {
        public FlowOperateBefore(TEntity entity, FlowOperateInput operateInput)
        {
            Entity = entity;
            OperateInput = operateInput;
        }

        /// <summary>
        /// 单据实体
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public FlowOperateInput OperateInput { get; }
    }
}
