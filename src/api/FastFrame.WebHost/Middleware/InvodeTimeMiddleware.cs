using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Cache;
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

            var log = new Entity.Basis.ApiRequestLog
            {
                IPAddress = context.Connection.RemoteIpAddress?.ToString(),
                Milliseconds = 0,
                Path = context.Request.Path,
                RequestTime = begin_time,
                StatusCode = 200,
                UserName = null,
                User_Id = null,
                Id = IdGenerate.NetId(),
                RequestLength = context.Request.ContentLength
            };

            context.Items.Add(Entity.Basis.ApiRequestLog.ListKeyName, log.Id);

            context.Response.OnStarting(() =>
            {
                stopwatch?.Stop();
                context.Response.Headers.Add("invoke-elapsed-milliseconds", stopwatch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            });

            await next(context);

            var curr = context.RequestServices.GetService<Infrastructure.Interface.IApplicationSession>().CurrUser;
            log.StatusCode = context.Response.StatusCode;
            log.Milliseconds = stopwatch.ElapsedMilliseconds;
            log.UserName = curr?.Name ?? "/";
            log.User_Id = curr?.Id;

            await context
              .RequestServices
              .GetService<ICacheProvider>()
              .ListPushAsync(Entity.Basis.ApiRequestLog.ListKeyName, log);
        }
    }
}
