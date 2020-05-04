using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class PermissionController : BaseController
    {
        private readonly IPermissionChecker service;

        public PermissionController(IPermissionChecker service)  
        {
            this.service = service;
        }  

        /// <summary>
        /// 被授予的权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PermissionDefinition>> Permissions()
        {
            return await service.GetGrantedPermissions();
        } 
    }
}
