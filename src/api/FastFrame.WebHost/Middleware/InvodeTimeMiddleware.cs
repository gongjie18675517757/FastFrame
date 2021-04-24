using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Middleware
{
    /// <summary>
    /// 写入接口执行时间
    /// </summary>
    public class InvodeTimeMiddleware
    {
        private readonly RequestDelegate next;

        public InvodeTimeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                context.Response.Headers.Add("invoke-elapsed-milliseconds", stopwatch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            }); 

            await next(context);
        }
    }
}
