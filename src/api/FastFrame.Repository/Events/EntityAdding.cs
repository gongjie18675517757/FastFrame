using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Repository.Events
{
    /// <summary>
    /// 实体新增时
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityAdding<T>(T data, params object[] args) : BaseEventData<T> where T:IEntity
    {
        public T Data { get; } = data;
        public object[] Args { get; } = args;
    }
}
