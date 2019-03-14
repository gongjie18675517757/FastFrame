using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 更新前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainUpdateing<T> : BaseEventData<T>  
    {
        public DoMainUpdateing(T data, params object[] args)  
        {
            Data = data;
            Args = args;
        }

        public T Data { get; }
        public object[] Args { get; }
    }
}
