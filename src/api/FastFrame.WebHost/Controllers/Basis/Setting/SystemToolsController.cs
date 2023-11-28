using FastFrame.Application;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.WebHost.Controllers.Basis
{
    /// <summary>
    /// 系统工具
    /// </summary>
    public class SystemToolsController(IServiceProvider serviceProvider) : BaseController
    {
        [Permission("View", "查看系统工具")]
        [HttpPost]
        public void View()
        {

        }

        [Permission("ReCalcTreeCode", "重算树状码")]
        [HttpPost]
        public Task ReCalcTreeCode()
        {
            return serviceProvider.GetService<ITreeHandleService>().ReCalcTreeCodeAsync();
        }
    }
}
