using FastFrame.Application;
using FastFrame.Application.Basis;
using FastFrame.Entity.Basis;

namespace FastFrame.WebHost.Controllers.Basis
{
    /// <summary>
    /// 接口请求记录
    /// </summary>
    public class ApiRequestLogController : BaseController<ApiRequestLog>
    {
        public ApiRequestLogController(ApiRequestLogService service) : base(service)
        {
        }
    }
}
