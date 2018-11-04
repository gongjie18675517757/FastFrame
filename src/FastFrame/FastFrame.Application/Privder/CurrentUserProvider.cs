using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using System.Linq;
using EasyCaching.Core;

namespace FastFrame.Application.Privder
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEasyCachingProvider cachingProvider;
        private string tokenName = "_code";
        private string token;
        private ICurrUser currUser;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor, IEasyCachingProvider cachingProvider)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cachingProvider = cachingProvider;
        }
        public string GetCurrOrganizeId()
        {
            return httpContextAccessor.HttpContext.GetRouteValue("organize")?.ToString();
        }

        public ICurrUser GetCurrUser()
        {
            if (currUser == null)
            {
                var token = GetToken();
                if (!token.IsNullOrWhiteSpace())
                {
                    var cacheValue = cachingProvider.Get<CurrUser>(token);
                    if (cacheValue.HasValue)
                        currUser = cacheValue.Value;
                }
            }
            return currUser;
        }

        public async Task Login(ICurrUser currUser)
        {
            await cachingProvider.SetAsync(currUser.ToKen, currUser, TimeSpan.FromDays(1));
            httpContextAccessor.HttpContext.Response.Headers.Add(tokenName, currUser.ToKen);
            httpContextAccessor.HttpContext.Response.Cookies.Delete(tokenName);
            httpContextAccessor.HttpContext.Response.Cookies.Append(tokenName, currUser.ToKen, new CookieOptions()
            {
                Expires = new DateTimeOffset(DateTime.Now.AddYears(1)),
                HttpOnly = true,
            });
        }

        public async Task LogOut(ICurrUser currUser)
        {
            var token = GetToken();
            await cachingProvider.RemoveAsync(currUser.ToKen);
        }

        private string GetToken()
        {
            if (!token.IsNullOrWhiteSpace())
                return token;
            token = string.Empty;
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(tokenName, out var headerValue))
            {
                token = headerValue.First();
            }
            if (token.IsNullOrWhiteSpace() && httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(tokenName, out var cookieValue))
            {
                token = cookieValue;
            }
            if (token.IsNullOrWhiteSpace() && httpContextAccessor.HttpContext.Request.Query.TryGetValue(tokenName, out var queryValue))
            {
                token = queryValue.First();
            }

            return token;
        }
    }
}