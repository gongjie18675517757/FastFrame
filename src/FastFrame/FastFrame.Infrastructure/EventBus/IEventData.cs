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

    /// <summary>
    /// 实体变化事件数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityEventData<T> : IEventData<T>
    {
        protected EntityEventData(T data,params object[] args)
        {
            Data = data;
            Args = args;
        }

        public virtual T Data { get; }

        public virtual object[] Args { get; }
    }

    /// <summary>
    /// 实体新增时
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityAdding<T> : EntityEventData<T>
    {
        public EntityAdding(T data, params object[] args) : base(data, args)
        {
        }
    } 
}
