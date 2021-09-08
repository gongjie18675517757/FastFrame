using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 删除后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleted<T> : BaseEventData<T>  
    {
        public string Id { get; set; }
    }
}
