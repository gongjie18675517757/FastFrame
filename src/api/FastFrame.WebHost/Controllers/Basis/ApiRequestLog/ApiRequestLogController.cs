using FastFrame.Application;
using FastFrame.Entity.Basis;

namespace FastFrame.WebHost.Controllers.Basis
{
    /// <summary>
    /// 接口请求记录
    /// </summary>
    public class ApiRequestLogController : BaseController<ApiRequestLog>
    {
        public ApiRequestLogController(IPageListService<ApiRequestLog> service) : base(service)
        {
        } 
    }
}
