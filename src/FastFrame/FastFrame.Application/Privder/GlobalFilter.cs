using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace FastFrame.Application.Privder
{
    public class GlobalFilter : IAsyncExceptionFilter, IAsyncAuthorizationFilter
    {
        private readonly ICurrentUserProvider operaterProvider;
        private readonly PermissionService permissionService;
        private readonly IDescriptionProvider descriptionProvider;
        private const string StopwatchName = "Stopwatch";

        public GlobalFilter(ICurrentUserProvider operaterProvider,
            PermissionService permissionService,
            IDescriptionProvider descriptionProvider)
        {
            this.operaterProvider = operaterProvider;
            this.permissionService = permissionService;
            this.descriptionProvider = descriptionProvider;
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter)))
            {
                if (operaterProvider.GetCurrUser() == null)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new ObjectResult(new { Message = "未登陆" });
                }
                else
                {
                    if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        var permissionAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<PermissionAttribute>();
                        var permissionAttribute2 = controllerActionDescriptor.MethodInfo.GetCustomAttribute<PermissionAttribute>();
                        if (permissionAttribute != null && permissionAttribute2 != null)
                        {
                            var moduleName = permissionAttribute.EnCode;
                            var methodNames = permissionAttribute2.AllEnCodes;
                            var exists= await permissionService.ExistPermission(moduleName, methodNames);
                            if (!exists)
                            {
                                context.HttpContext.Response.StatusCode = 403;
                                context.Result = new ObjectResult(new { Message = "权限不足" });
                            }
                        }
                    }
                    operaterProvider.Refresh();
                }
            } 
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
                context.Result = new ObjectResult(context.Exception);
#endif
            }
        }
    }
}