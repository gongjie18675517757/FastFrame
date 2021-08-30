using System;
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
        private static readonly IDictionary<string, TaskCompletionSource<TResult>> dic
           = new Dictionary<string, TaskCompletionSource<TResult>>(100);

        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        public static void MakeTaskCompletionSource(string id)
        {
            var taskCompletionSource = new TaskCompletionSource<TResult>();
            lock (dic)
            {
                dic.Add(id, taskCompletionSource);
            }
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
            Task<TResult> task = null;
            lock (dic)
            {
                if (dic.TryGetValue(id, out var taskCompletionSource))
                    task = taskCompletionSource.Task;
            }

            if (task == null)
                throw new MsgException("任务丢失");

            /*判断超时*/
            var any_task = millisecondsDelay == null ?
                            task :
                            await Task.WhenAny(task, Task.Delay(millisecondsDelay.Value));

            lock (dic)
            {
                if (dic.TryGetValue(id, out _))
                    dic.Remove(id);
            }

            if (any_task == task)
                return await task;
            else
                throw new MsgException(timeoutErrorMsg);
        }

        /// <summary>
        /// 设置任务完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static void SetTaskCompletionSource(string id, Action<TaskCompletionSource<TResult>> action)
        {
            lock (dic)
            {
                if (dic.TryGetValue(id, out var task))
                {
                    action(task);
                    dic.Remove(id);
                }
            }
        }
    }
}
