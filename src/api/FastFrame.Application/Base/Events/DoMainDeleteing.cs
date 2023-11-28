using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 删除前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleteing<T>(string id, object Data) : BaseEventData<T> 
    {
        public string Id { get; } = id;
        public object Data { get; } = Data;
    }
}
