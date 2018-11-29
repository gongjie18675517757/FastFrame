using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 删除前
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleteing<T> : BaseEventData<T> where T : IDto
    {
        public DoMainDeleteing(T data, params object[] args) : base(data, args)
        {
        }
    }
}
