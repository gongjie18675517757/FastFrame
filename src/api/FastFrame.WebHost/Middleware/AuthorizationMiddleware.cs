using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.WebHost.Middleware
{
    /// <summary>
    /// 身份授权验证中间件
    /// </summary>
    public class AuthorizationMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null && endpoint.Metadata.GetMetadata<ControllerAttribute>() != null)
            {
                /*是否允许匿名访问*/
                var isAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;


                if (!isAnonymous)
                {
                    var curr = context.RequestServices.GetService<IApplicationSession>().CurrUser;
                    if (curr == null)
                    {
                        context.Response.StatusCode = 401;
                        //context.Response.ContentType = "application/json";
                        await context.Response.WriteJsonAsync(new { Message = "未登陆" });
                        return;
                    }
                }
            }

            await next(context);
        }
    }
}
