namespace FastFrame.Repository
{
    /// <summary>
    /// 事件侦听管理器
    /// </summary>
    public class EventListenManger : IDisposable
    {
        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, HashSet<Func<IServiceProvider, Task>>> listens;

        public EventListenManger()
        {
            listens = new System.Collections.Concurrent.ConcurrentDictionary<string, HashSet<Func<IServiceProvider, Task>>>();
        }

        /// <summary>
        /// 监听事件列表
        /// </summary>
        public IReadOnlyCollection<Func<IServiceProvider, Task>> this[string key]
        {
            get
            {
                if (listens.TryGetValue(key, out var funcs))
                    return funcs;

                return new List<Func<IServiceProvider, Task>>();
            }
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="func"></param>
        public void AddEventListen(string key, Func<IServiceProvider, Task> func)
        {
            if (!listens.TryGetValue(key, out var funcs))
            {
                funcs = new HashSet<Func<IServiceProvider, Task>>();
                while (true)
                {
                    if (listens.TryAdd(key, funcs))
                        break;
                }
            }

            funcs.Add(func);
        }

        public void Dispose()
        {
            foreach (var item in listens)
                item.Value.Clear();

            listens.Clear();

            GC.SuppressFinalize(this);
        }
    }

    public interface EventListenTrigger
    {
        Task TriggerEvent(IServiceProvider loader);
    }
}
