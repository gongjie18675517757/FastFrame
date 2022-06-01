using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IModuleDesProvider moduleDesProvider;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IModuleDesProvider moduleDesProvider)
        {
            this.next = next;
            this.logger = logger;
            this.moduleDesProvider = moduleDesProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items.TryGetValue(Entity.Basis.ApiRequestLog.ListKeyName, out var log_id);
            try
            {
                await next(context);
            }
            catch (UniqueException uniqueException)
            {
                var typeDescription = moduleDesProvider.GetClassDescription(uniqueException.Type);
                var propDescriptions = new string[uniqueException.PropNames.Length];
                for (int i = 0; i < uniqueException.PropNames.Length; i++)
                {
                    propDescriptions[i] = moduleDesProvider.GetPropertyDescription(uniqueException.Type, uniqueException.PropNames[i]);
                }

                context.Response.StatusCode = 400;
                await context.Response.WriteJsonAsync(new
                {
                    Message = $"{typeDescription}:{string.Join("+", propDescriptions)} 重复!",
                    Code = -2
                });
            }
            catch (MsgException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteJsonAsync(new
                {
                    ex.Message,
                    ex.Code
                });
            }
            catch (NotFoundException)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                await context.Response.WriteJsonAsync(new
                {
                    Message = "未匹配到内容",
                    Code = 404
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

#if DEBUG
                await context.Response.WriteJsonAsync(new
                {
                    Message = "出现了点小问题啦。。请联系管理员。。",
                    Code = -1,
                    log_id
                });

#else
                await context.Response.WriteJsonAsync(new
                {
                    ex.Message,
                    ex.StackTrace,
                    Code = -1,
                    log_id
                });
#endif
                logger.LogError(ex, $"log_id:{log_id},{ex.Message}");
            }
        }
    }

}
