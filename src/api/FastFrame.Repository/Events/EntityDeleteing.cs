using FastFrame.Entity;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Repository.Events
{
    /// <summary>
    /// 实体被删除时
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDeleteing<T>(T data, params object[] args) : BaseEventData<T> where T : IEntity
    {
        public T Data { get; } = data;
        public object[] Args { get; } = args;
    }
}
