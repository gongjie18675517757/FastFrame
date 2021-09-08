using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 添加后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainAdded<T> : BaseEventData<T>  
    {
        public string Id { get; set; }
    }
}
