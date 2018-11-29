using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 更新前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainUpdateing<T> : BaseEventData<T> where T : IDto
    {
        public DoMainUpdateing(T data, params object[] args) : base(data, args)
        {
        }
    }
}
