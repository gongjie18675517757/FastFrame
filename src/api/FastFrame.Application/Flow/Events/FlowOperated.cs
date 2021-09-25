using FastFrame.Entity;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure.EventBus;
using System.Collections.Generic;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程操作完成
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class FlowOperated<TEntity> : IEventData where TEntity : IHaveCheck
    {
        public FlowOperated(TEntity entity, FlowOperateInput operateInput, IEnumerable<FlowStep> flowProcess)
        {
            Entity = entity;
            FlowProcess = flowProcess;
            OperateInput = operateInput;
        }

        /// <summary>
        /// 单据实体
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// 审批过程
        /// </summary>
        public IEnumerable<FlowStep> FlowProcess { get; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public FlowOperateInput OperateInput { get; }
    }
}
