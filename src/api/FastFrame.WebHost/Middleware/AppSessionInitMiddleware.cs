using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Buffers;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;

namespace FastFrame.WebHost.Middleware
{
    public class AppSessionInitMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            await context.Request.HttpContext.RequestServices.GetService<IApplicationSession>().InitAsync();
            await next(context);
        }
    }  
}
