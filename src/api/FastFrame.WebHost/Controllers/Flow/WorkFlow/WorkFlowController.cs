using FastFrame.Application;
using FastFrame.Entity;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Flow
{
    public partial class WorkFlowController
    {
        /// <summary>
        /// 获取最大版本
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        [Permission("Add")]
        [HttpGet("{moduleName}")]
        public async Task<int> GetLastVersion(string moduleName)
        {
            return await service.GetLastVersion(moduleName);
        }

        /// <summary>
        /// 获取树形视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId()
        {
            return await service.GetChildrenBySuperId();
        }

        [Permission(new[] { "Add", "Update" })]
        [HttpGet]
        public async Task<IEnumerable<IViewModel>> CheckerList(FlowNodeCheckerEnum checkerEnum, string moduleName, string kw)
        {
            return await service.CheckerList(checkerEnum, moduleName, kw);
        }

        [Permission(new[] { "Add", "Update" })]
        [HttpGet("{entityName}")]
        public async Task<IEnumerable<IViewModel>> RelateKvs(string entityName, string kw)
        {
            return await service.RelateKvs(entityName, kw);
        }

        [HttpPost("{id}")]
        [Permission("ToggleEnable", "切换启用/停用")]
        public async Task ToggleEnable(string id)
        {
            await service.ToggleEnable(id);
        }
    }
}
