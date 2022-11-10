using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 异步任务中心
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public static class TaskCompletionSourceCenter<TResult>
    {
        private static readonly ConcurrentDictionary<string, IAwaitableCompletionSource<TResult>> dic = new();

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        public static void MakeTaskCompletionSource(string id)
        {
            var taskCompletionSource = AwaitableCompletionSource.Create<TResult>();
            dic.TryAdd(id, taskCompletionSource);
        }

        /// <summary>
        /// 等待任务完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="millisecondsDelay"></param>
        /// <param name="timeoutErrorMsg"></param>
        /// <returns></returns>
        public static async Task<TResult> DelayTaskCompletionSource(string id, int? millisecondsDelay, string timeoutErrorMsg)
        {
            if (dic.TryGetValue(id, out var taskCompletionSource))
            {
                if (millisecondsDelay != null)
                    taskCompletionSource.TrySetExceptionAfter(new MsgException(timeoutErrorMsg), TimeSpan.FromSeconds(millisecondsDelay.Value));
            }
            else
            {
                throw new MsgException("任务丢失");
            }

            var result = await taskCompletionSource.Task;

            if (dic.TryGetValue(id, out _))
            {
                dic.Remove(id, out var awaitable);
                awaitable.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 设置任务完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void SetTaskCompletionSource(string id, Action<IAwaitableCompletionSource<TResult>> action)
        {
            if (dic.TryGetValue(id, out var task))
            {
                action(task);
                dic.Remove(id, out var awaitable);
                awaitable.Dispose();
            }
        }
    }
}
