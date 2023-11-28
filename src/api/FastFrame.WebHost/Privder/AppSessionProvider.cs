using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Identity;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class AppSessionProvider(IHttpContextAccessor httpContextAccessor,
                                    IMemoryCache memoryCache,
                                    IHostEnvironment hostEnvironment) : IApplicationSession
    {
        private Tenant tenant;

        public ICurrUser CurrUser { get; private set; }

        public string Tenant_Id => tenant?.Tenant_Id;

        public string ApplicationRootPath => hostEnvironment.IsProduction() ?
                    hostEnvironment?.ContentRootPath ?? AppDomain.CurrentDomain.BaseDirectory :
                    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string[] stringArray = ["X-ORIGINAL-HOST", "Origin", "Referer"];

        private bool TryGetHeaderValue(string[] headerNames, out string host)
        {
            host = string.Empty;
            foreach (var headerName in headerNames)
            {
                if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out var sv) && sv.Count != 0)
                {
                    var arr = sv.ToArray();
                    host = arr.FirstOrDefault();
                    return true;
                }
            }
            return false;
        }

        private string GetHost()
        {
            if (!TryGetHeaderValue(stringArray, out var host))
                host = httpContextAccessor.HttpContext.Request.Host.Value;
            return host;
        }

        public void Login(ICurrUser currUser)
        {
            var base64 = currUser.ToJson().ToBase64();

            httpContextAccessor.HttpContext.Response.Headers.Append(ConstValuePool.Token_Name, base64);
            httpContextAccessor.HttpContext.Response.Cookies.Delete(ConstValuePool.Token_Name);
            httpContextAccessor.HttpContext.Response.Cookies.Append(ConstValuePool.Token_Name, base64, new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(1)),
                HttpOnly = true,
                //Domain = ".localhost",
                Path = "/",
            });
        }

        public void LogOut()
        {
            if (CurrUser != null)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete(ConstValuePool.Token_Name);
                httpContextAccessor.HttpContext.RequestServices.GetService<IIdentityManager>().SetTokenFailure(CurrUser.ToKen);
            }
        }

        public void RefreshIdentity()
        {
            httpContextAccessor.HttpContext.RequestServices.GetService<IIdentityManager>().RefreshToken(CurrUser.ToKen);
            Login(CurrUser);
        }

        private string GetIdentity()
        {
            var request = httpContextAccessor.HttpContext.Request;
            var identity = string.Empty;
            if (request.Headers.TryGetValue(ConstValuePool.Token_Name, out var headerValue))
                identity = headerValue.First();
            if (identity.IsNullOrWhiteSpace() && request.Cookies.TryGetValue(ConstValuePool.Token_Name, out var cookieValue))
                identity = cookieValue;
            if (identity.IsNullOrWhiteSpace() && request.Query.TryGetValue(ConstValuePool.Token_Name, out var queryValue))
                identity = queryValue.First();
            return identity;
        }

        /// <summary>
        /// 获取当前连接的IP
        /// </summary>
        /// <returns></returns>
        public IPAddress GetIPAddress()
        {
            return httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress;
        }

        public async Task InitAsync()
        {
            var identity = GetIdentity();
            if (!identity.IsNullOrWhiteSpace())
            {
                var curr = identity.FromBase64().ToObject<CurrUser>();
                if (curr != null)
                    if (await httpContextAccessor.HttpContext.RequestServices.GetService<IIdentityManager>().ExistsTokenAsync(curr.ToKen, GetIPAddress()))
                        CurrUser = curr;
            }

            var host = GetHost();

            if (memoryCache.TryGetValue<Tenant[]>(ConstValuePool.CacheTenant, out var tenants) &&
                memoryCache.TryGetValue<TenantHost[]>(ConstValuePool.CacheTenantHost, out var tenantHosts))
            {
                var tenantHost = tenantHosts.FirstOrDefault(v => v.Host == host);
                tenantHost ??= tenantHosts.FirstOrDefault(v => v.Host == "*");
                tenant = tenants.FirstOrDefault(v => v.Id == tenantHost?.Id) ?? tenants.FirstOrDefault(v => v.UrlMark == "*");
            }
            else
            {
                tenant = new Tenant();
            }
        }
    }
}