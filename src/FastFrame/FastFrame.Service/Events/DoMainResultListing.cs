using FastFrame.Infrastructure.EventBus;
using System.Collections.Generic;

namespace FastFrame.Service.Events
{
    /// <summary>
    /// 返回列表数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoMainResultListing<T> : BaseEventData<T>
    {
        public DoMainResultListing(IEnumerable<T> data, params object[] args)
        {
            Data = data;
            Args = args;
        }

        public IEnumerable<T> Data { get; }
        public object[] Args { get; }
    }
}
