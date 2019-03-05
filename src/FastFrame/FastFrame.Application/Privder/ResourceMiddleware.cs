using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FastFrame.Application.Privder
{
    public class ResourceMiddleware
    {
        private readonly RequestDelegate next;
        //private readonly Regex reg;

        public ResourceMiddleware(RequestDelegate next)
        {
            this.next = next;
            //reg = new Regex("^/resource/(.+)$");
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.HasValue && path.Value.ToLower().StartsWith("/api/resource/get/") &&
                    context.Request.Headers.ContainsKey("If-Modified-Since"))
            {
                context.Response.StatusCode = 304;
                return;
            }

            await next(context);
        }
    }
}
