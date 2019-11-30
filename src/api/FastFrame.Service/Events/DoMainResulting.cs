using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainResulting<T>: BaseEventData<T>
    {
        public DoMainResulting(T data, params object[] args)
        {
            Data = data;
            Args = args;
        }

        public T Data { get; }
        public object[] Args { get; }
    }
}
