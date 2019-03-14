using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 删除前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleteing<T> : BaseEventData<T> 
    {
        public DoMainDeleteing(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
