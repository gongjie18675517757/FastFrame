using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 删除前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleteing<T> : BaseEventData<T> 
    {
        public DoMainDeleteing(string id,object Data)
        {
            Id = id;
            this.Data = Data;
        }

        public string Id { get; }
        public object Data { get; }
    }
}
