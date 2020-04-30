using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 添加后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainAdded<T> : BaseEventData<T>  
    {
        public DoMainAdded(T data, params object[] args)
        {
            Data = data;
            Args = args;
        }

        public T Data { get; }
        public object[] Args { get; }
    }
}
