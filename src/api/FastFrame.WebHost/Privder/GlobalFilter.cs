﻿using AspectCore.DynamicProxy;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class GlobalFilter : IAsyncExceptionFilter, IAsyncAuthorizationFilter
    {
        private readonly IAppSessionProvider appSession;
        private readonly PermissionService permissionService;
        private readonly IDescriptionProvider descriptionProvider;
        private readonly ILogger<GlobalFilter> logger; 

        public GlobalFilter(
            IAppSessionProvider appSession,
            PermissionService permissionService,
            IDescriptionProvider descriptionProvider,
            ILogger<GlobalFilter> logger)
        {
            this.appSession = appSession;
            this.permissionService = permissionService;
            this.descriptionProvider = descriptionProvider;
            this.logger = logger;
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            /*取当前用户*/
            var curr = appSession.CurrUser;

            /*是否标记为可以匿名访问(EveryoneAccessFilter在.NET CORE 3.*下,不知道怎么获取不到了)*/
            if (curr == null && context.Filters.Any(x => x.GetType() == typeof(EveryoneAccessAttribute)))
            {
                return;
            }
            /*未标记匿名访问，未登陆时，则不允许访问*/
            else if (curr == null)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new ObjectResult(new { Message = "未登陆" });
                return;
            }

            /*管理员无须验证，直接跳出*/
            if (curr.IsAdmin)
                return;

            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var permissionAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<PermissionAttribute>();
                var permissionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes<PermissionAttribute>();
                if (permissionAttribute != null && permissionAttributes != null && permissionAttributes.Any())
                {
                    var moduleName = permissionAttribute.EnCode;
                    var methodNames = permissionAttributes.SelectMany(v => v.AllEnCodes);

                    /*验证权限*/
                    if (!await permissionService.ExistPermission(moduleName, methodNames.ToArray()))
                    {
                        context.HttpContext.Response.StatusCode = 403;
                        context.Result = new ObjectResult(new { Message = "权限不足" });
                    }
                }
            }
        }

        /// <summary>
        /// 异常验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            /*写入Log*/
            context.HttpContext.Response.StatusCode = 400;
            var ex = context.Exception;
            if (ex is AspectInvocationException aspectInvocationException)
                ex = ex.InnerException;

            /*模型字段重复异常*/
            if (ex is UniqueException uniqueException)
            {
                var typeDescription = descriptionProvider.GetClassDescription(uniqueException.Type);
                var propDescriptions = new string[uniqueException.PropNames.Length];
                for (int i = 0; i < uniqueException.PropNames.Length; i++)
                {
                    propDescriptions[i] = descriptionProvider.GetPropertyDescription(uniqueException.Type, uniqueException.PropNames[i]);
                }
                context.Result = new ObjectResult(new
                {
                    Message = $"{typeDescription}:{string.Join("+", propDescriptions)} 重复!",
                    Code = -2
                });
            }
            /*预定义异常*/
            else if (ex is MsgException msgException)
            {
                context.Result = new ObjectResult(new
                {
                    msgException.Message,
                    msgException.Code
                });
            }
            /*未定义异常*/
            else
            {
                context.Result = new ObjectResult(new
                {
                    Message = "出现了点小问题啦。。请联系管理员。。",
                    Code = -1
                });
#if DEBUG
                context.Result = new ObjectResult(new
                {
                    ex.Message,
                    ex.StackTrace,
                    Code = -1
                });
#endif
            }


            logger.LogError(ex, ex.Message);
            return Task.CompletedTask;
        }
    } 
}