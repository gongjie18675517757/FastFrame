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
        private const string StopwatchName = "Stopwatch";

        public GlobalFilter(ICurrentUserProvider operaterProvider)
        {
            this.operaterProvider = operaterProvider;
        }  
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //if (!context.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter)))
            {
                if (operaterProvider.GetCurrUser() == null)
                {
                    //context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
                    //context.Result = new ObjectResult(new { Message = "未登陆" });
                    //context.HttpContext.Response.StatusCode = 401;
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
            context.Result = new ObjectResult(new { Message = context.Exception.Message });
            context.HttpContext.Response.StatusCode = 401;
            await Task.CompletedTask;
        } 
    }
}