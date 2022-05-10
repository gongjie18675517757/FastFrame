using FastFrame.Application.Basis;
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
            var begin_time = DateTime.Now;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                context.Response.Headers.Add("invoke-elapsed-milliseconds", stopwatch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            });

            await next(context);

            //stopwatch.Stop();
            //context.Response.Headers.Add("invoke-elapsed-milliseconds", stopwatch.ElapsedMilliseconds.ToString());
            //return Task.CompletedTask;

            if (context.Request.Path.StartsWithSegments("/api"))
            {
                var curr = context.RequestServices.GetService<Infrastructure.Interface.IApplicationSession>().CurrUser;
                context.RequestServices.GetService<ApiRequestLogService>().BackgroundInsert(new Entity.Basis.ApiRequestLog
                {
                    IPAddress = context.Connection.RemoteIpAddress?.ToString(),
                    Milliseconds = stopwatch.ElapsedMilliseconds,
                    Path = context.Request.Path,
                    RequestTime = begin_time,
                    StatusCode = context.Response.StatusCode,
                    UserName = curr?.Name ?? "/",
                    User_Id = curr?.Id,
                });
            }
           
        }
    }
}
