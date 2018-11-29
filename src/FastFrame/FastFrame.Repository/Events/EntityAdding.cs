using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Repository.Events
{
    /// <summary>
    /// 实体新增时
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityAdding<T> : BaseEventData<T> where T:IEntity
    {
        public EntityAdding(T data, params object[] args) : base(data, args)
        {
        }
    }
}
