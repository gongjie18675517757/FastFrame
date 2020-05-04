using CSRedis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class AppSessionProvider : IAppSessionProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMemoryCache memoryCache;
        private readonly CSRedisClient cSRedisClient;
        private readonly IHostEnvironment hostEnvironment;
        private const string Token_Name = "Authorize";
        private Tenant tenant;

        public AppSessionProvider(IHttpContextAccessor httpContextAccessor,
                                   IMemoryCache memoryCache,
                                   CSRedisClient cSRedisClient,
                                   IHostEnvironment hostEnvironment)
        {
            this.httpContextAccessor = httpContextAccessor;

            this.memoryCache = memoryCache;
            this.cSRedisClient = cSRedisClient;
            this.hostEnvironment = hostEnvironment;
        }

        public ICurrUser CurrUser { get; private set; }

        public string Tenant_Id => tenant?.Tenant_Id;

        public string ApplicationRootPath => hostEnvironment.IsProduction() ? 
                    hostEnvironment?.ContentRootPath ?? AppDomain.CurrentDomain.BaseDirectory : 
                    AppDomain.CurrentDomain.BaseDirectory;

        private bool TryGetHeaderValue(string[] headerNames, out string host)
        {
            host = string.Empty;
            foreach (var headerName in headerNames)
            {
                if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out var sv) && sv.Any())
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
            if (!TryGetHeaderValue(new string[] { "X-ORIGINAL-HOST", "Origin", "Referer" }, out var host))
                host = httpContextAccessor.HttpContext.Request.Host.Value;
            return host;
        }

        public Task LoginAsync(ICurrUser currUser)
        {
            var base64 = currUser.ToJson().ToBase64();

            httpContextAccessor.HttpContext.Response.Headers.Add(Token_Name, base64);
            httpContextAccessor.HttpContext.Response.Cookies.Delete(Token_Name);
            httpContextAccessor.HttpContext.Response.Cookies.Append(Token_Name, base64, new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(1)),
                HttpOnly = true,
                //Domain = ".localhost",
                Path = "/",
            });

            return Task.CompletedTask;
        }

        public async Task LogOutAsync()
        {
            if (CurrUser != null)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete(Token_Name);
                await cSRedisClient.DelAsync(CurrUser.ToKen);
                await httpContextAccessor.HttpContext.RequestServices.GetService<IIdentityManager>().SetTokenFailureAsync(CurrUser.ToKen);
            }
        }

        public async Task RefreshIdentityAsync()
        {
            await cSRedisClient.SetAsync(CurrUser.ToKen, CurrUser, 60 * 60 * 24);
            await httpContextAccessor.HttpContext.RequestServices.GetService<IIdentityManager>().RefreshTokenAsync(CurrUser.ToKen);
            await LoginAsync(CurrUser);
        }

        private string GetIdentity()
        {

            var request = httpContextAccessor.HttpContext.Request;
            var identity = string.Empty;
            if (request.Headers.TryGetValue(Token_Name, out var headerValue))
                identity = headerValue.First();
            if (identity.IsNullOrWhiteSpace() && request.Cookies.TryGetValue(Token_Name, out var cookieValue))
                identity = cookieValue;
            if (identity.IsNullOrWhiteSpace() && request.Query.TryGetValue(Token_Name, out var queryValue))
                identity = queryValue.First();
            return identity;
        }

        public async Task InitAsync()
        {
            var identity = GetIdentity();
            if (!identity.IsNullOrWhiteSpace())
            {
                var curr = identity.FromBase64().ToObject<CurrUser>();
                if (curr != null)
                {
                    if (await httpContextAccessor.HttpContext.RequestServices.GetService<IIdentityManager>().ExistsTokenAsync(curr.ToKen))
                    {
                        CurrUser = curr;
                    }
                }
            }

            var host = GetHost();

            if (memoryCache.TryGetValue<Tenant[]>("Cache:Multi-Tenant-List", out var tenants) &&
               memoryCache.TryGetValue<TenantHost[]>("Cache:Multi-TenantHost-List", out var tenantHosts))
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