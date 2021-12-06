using FastFrame.Application.Flow;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FastFrame.WebHost.Controllers.Flow
{
    /// <summary>
    /// 流程中心
    /// </summary>
    public partial class WorkFlowCenterController : BaseController
    {
        private readonly WorkFlowCenterService service;

        public WorkFlowCenterController(WorkFlowCenterService service)
        {
            this.service = service;
        }

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
        public Task<PageList<FlowInstance>> PageList(string qs)
        {
            return service.PageList(qs.ToObject<Pagination>(true));
        }
    }
}
