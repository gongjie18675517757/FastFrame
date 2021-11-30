using FastFrame.Entity;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FastFrame.Application.Flow
{
    /// <summary>
    /// 流程操作完成
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class FlowOperated<TEntity> : IEventData where TEntity : IHaveCheck
    {
        public FlowOperated(string key)
        {
            Key = key;
        }

        /// <summary>
        /// 单据主键
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="loader"></param>
        /// <returns></returns>
        public Task TriggerEvent(IServiceProvider loader)
        {
            loader.GetService<FastFrame.Infrastructure.Interface.IBackgroundJob>().SetTimeout<IEventBus>(v => v.TriggerEventAsync(this), null);
            return Task.CompletedTask;
        }
    }
}
