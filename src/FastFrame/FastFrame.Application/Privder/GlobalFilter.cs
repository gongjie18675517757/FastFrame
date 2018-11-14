using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Privder
{
    public class GlobalFilter : IAsyncExceptionFilter, IAsyncAuthorizationFilter
    {
        private readonly ICurrentUserProvider operaterProvider;
        private readonly IDescriptionProvider descriptionProvider;
        private const string StopwatchName = "Stopwatch";

        public GlobalFilter(ICurrentUserProvider operaterProvider, IDescriptionProvider descriptionProvider)
        {
            this.operaterProvider = operaterProvider;
            this.descriptionProvider = descriptionProvider;
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter)))
            {
                if (operaterProvider.GetCurrUser() == null)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new ObjectResult(new { Message = "未登陆" }); 
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 异常验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            /*写入Log*/
            //context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
            context.HttpContext.Response.StatusCode = 400;
            var ex = context.Exception;
            if (ex is UniqueException uniqueException)
            {
                var typeDescription = await descriptionProvider.GetClassDescription(uniqueException.Type);
                var propDescriptions = new string[uniqueException.PropNames.Length];
                for (int i = 0; i < uniqueException.PropNames.Length; i++)
                {
                    propDescriptions[i] = await descriptionProvider.GetPropertyDescription(uniqueException.Type, uniqueException.PropNames[i]);

                }
                context.Result = new ObjectResult(new { Message = $"{typeDescription}:{string.Join("+", propDescriptions)} 重复!" });
            }
            else
            {
                context.Result = new ObjectResult(new { Message = context.Exception.Message });
#if DEBUG
                context.Result = new ObjectResult(new { Message = context.Exception});
#endif
            }
        }
    }
}