using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.WebHost.Middleware
{
    public class AppSessionInitMiddleware
    {
        private readonly RequestDelegate next;

        public AppSessionInitMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Request.HttpContext.RequestServices.GetService<IAppSessionProvider>().InitAsync();
            await next(context);
        }
    }

   
}
