using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 添加后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainAdded<T> : BaseEventData<T> where T : IDto
    {
        public DoMainAdded(T data, params object[] args) : base(data, args)
        {
        }
    }
}
