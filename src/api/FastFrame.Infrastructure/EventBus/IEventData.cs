namespace FastFrame.Infrastructure.EventBus
{
    public interface IEventData
    {

    }
    /// <summary>
    /// 事件数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventData<T>: IEventData
    {  
    }
}
