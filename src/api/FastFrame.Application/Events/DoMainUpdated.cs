using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 更新后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainUpdated<T> : BaseEventData<T>  
    {
        public DoMainUpdated(T data, params object[] args)  
        {
            Data = data;
            Args = args;
        }

        public T Data { get; }
        public object[] Args { get; }
    }
}
