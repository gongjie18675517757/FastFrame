using FastFrame.Application.Account;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    [Permission("LoginLog", "登录身份")]
    public class LoginLogController : BaseController<LoginLogModel>
    {
        private readonly LoginLogServcie service;
        private readonly IdentityManagerService identityManagerService;

        public LoginLogController(LoginLogServcie service, IdentityManagerService identityManagerService) : base(service)
        {
            this.service = service;
            this.identityManagerService = identityManagerService;
        }

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
