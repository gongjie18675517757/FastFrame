using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Events
{
    /// <summary>
    /// 更新后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainUpdated<T> : BaseEventData<T>  
    {
        public string Id { get; set; }

        public object[] Parms { get; set; }
    }
}
