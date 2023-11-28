using FastFrame.Application;
using FastFrame.Application.Flow;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.WebHost.Controllers.Flow
{
    /// <summary>
    /// 流程中心
    /// </summary>
    public partial class WorkFlowCenterController(WorkFlowCenterService service) : BaseController
    {

        /// <summary>
        /// 流程操作
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{moduleName}")]
        public IAsyncEnumerable<FlowOperateOutput> FlowOperate(string moduleName, BatchFlowOperateInput input)
        {
            return service.HandleFlowOperate(moduleName, input);
        }

        /// <summary>
        /// 审批列表
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<IPageList<FlowInstance>> PageList(string qs)
        {
            return service.PageList(Pagination<FlowInstance>.FromJson(qs));
        }
    }
}
