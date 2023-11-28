using FastFrame.Application.Account;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    /// <summary>
    /// 登录身份
    /// </summary>
    public class LoginLogController(LoginLogServcie service, IdentityManagerService identityManagerService) 
        : BaseController<LoginLogModel>(service)
    {

        /// <summary>
        /// 设置身份失效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{token}")]
        [Permission("SetTokenFailure", "设置身份失效")]
        public async Task SetTokenFailure(string token)
        {
            await identityManagerService.SetTokenFailureAsync(token);
        }
    }
}
