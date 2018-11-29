using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Repository.Events
{
    /// <summary>
    /// 实体被更新时
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityUpdateing<T> : BaseEventData<T> where T : IEntity
    {
        public EntityUpdateing(T data, params object[] args) : base(data, args)
        {
        }
    }
}
