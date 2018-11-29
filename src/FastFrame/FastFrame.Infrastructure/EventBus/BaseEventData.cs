namespace FastFrame.Infrastructure.EventBus
{
    /// <summary>
    /// 实体变化事件数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEventData<T> : IEventData<T>
    {
        protected BaseEventData(T data,params object[] args)
        {
            Data = data;
            Args = args;
        }

        public virtual T Data { get; }

        public virtual object[] Args { get; }
    }
}
