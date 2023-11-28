using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 更新前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainUpdateing<T>(T data, params object[] args) : BaseEventData<T>  
    {
        public T Data { get; } = data;
        public object[] Args { get; } = args;
    }
}
