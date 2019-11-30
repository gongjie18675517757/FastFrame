using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 删除后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleted<T> : BaseEventData<T>  
    {
        public DoMainDeleted(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
