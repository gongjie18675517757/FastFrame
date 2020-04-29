using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;


namespace FastFrame.WebHost
{
    /// <summary>
    /// 所有人可以访问
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EveryoneAccessAttribute : System.Attribute, IAsyncAuthorizationFilter
    {

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            return Task.CompletedTask;
        }
    }
}
