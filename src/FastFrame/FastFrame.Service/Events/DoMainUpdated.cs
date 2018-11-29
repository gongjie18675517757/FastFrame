using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 更新后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainUpdated<T> : BaseEventData<T> where T : IDto
    {
        public DoMainUpdated(T data, params object[] args) : base(data, args)
        {
        }
    }
}
