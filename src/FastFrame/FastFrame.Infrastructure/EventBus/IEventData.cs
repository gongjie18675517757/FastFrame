namespace FastFrame.Infrastructure.EventBus
{
    /// <summary>
    /// 事件数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventData<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        T Data { get; }

        /// <summary>
        /// 其它参数
        /// </summary>
        object[] Args { get; }
    }
}
