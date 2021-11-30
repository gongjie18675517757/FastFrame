using FastFrame.Entity;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程操作时(状态生成完成,事务提交前)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class FlowOperateing<TEntity> : IEventData where TEntity : IHaveCheck
    {
        public FlowOperateing(TEntity entity, FlowInstance flowInstance, FlowOperateInput operateInput)
        {
            Entity = entity;
            FlowInstance = flowInstance;
            OperateInput = operateInput;
        }

        /// <summary>
        /// 单据实体
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// 流程实例
        /// </summary>
        public FlowInstance FlowInstance { get; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public FlowOperateInput OperateInput { get; }
    }
}
