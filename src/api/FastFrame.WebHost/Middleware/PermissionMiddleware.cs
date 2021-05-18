using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Middleware
{
    /// <summary>
    /// 权限处理中间件
    /// </summary>
    public class PermissionMiddleware
    {
        private readonly RequestDelegate next;

        public PermissionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var appSessionProvider = context.Request.HttpContext.RequestServices.GetService<IAppSessionProvider>();
                var currUser = appSessionProvider.CurrUser;
#if RELEASE
                if (!currUser.IsAdmin)
#endif
                {
                    /*是否允许匿名访问*/
                    var isAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
                    if (!isAnonymous)
                    {
                        /*是否有权限要求*/
                        var permissionAttribute = endpoint.Metadata.GetOrderedMetadata<PermissionAttribute>();
                        if (permissionAttribute.Any())
                        {
                            /*权限组名称*/
                            var permissionGroupAttribute = endpoint.Metadata.GetMetadata<PermissionGroupAttribute>();
                            var groupName = permissionGroupAttribute?.Name;
                            if (groupName.IsNullOrWhiteSpace())
                            {
                                var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                                groupName = controllerActionDescriptor.ControllerTypeInfo.Name.Replace("Controller", "");
                            }

                            /*应用权限*/
                            var permissionKeys = permissionAttribute
                                .SelectMany(v => v.PermissionKeys)
                                .Select(v => v.Contains(".") ? v : $"{groupName}.{v}")
                                .ToArray();

                            /*是否被授权*/
                            var isGranted = await context.RequestServices.GetService<IPermissionChecker>().CheckIsGrantedAsync(permissionKeys);

                            /*未被授权时*/
                            if (!isGranted)
                            {
                                context.Response.StatusCode = 403;
                                await context.Response.WriteJsonAsync(new { Message = "未分配此应用的权限" });
                                return;
                            }
                        }

                    }
                }
            }
            await next(context);
        }
    }
}
