using FastFrame.Dto;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 删除后
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainDeleted<T> : BaseEventData<T> where T : IDto
    {
        public DoMainDeleted(T data, params object[] args) : base(data, args)
        {
        }
    }
}
