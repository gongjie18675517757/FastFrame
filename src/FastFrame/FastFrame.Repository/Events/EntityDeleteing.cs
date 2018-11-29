using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Repository.Events
{
    /// <summary>
    /// 实体被删除时
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDeleteing<T> : BaseEventData<T> where T : IEntity
    {
        public EntityDeleteing(T data, params object[] args) : base(data, args)
        {
        }
    }
}
