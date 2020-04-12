using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class UserController
    {
        /// <summary>
        /// 切换身份
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Permission(nameof(ToogleAdminIdentity), "切换身份")]
        public async Task<UserDto> ToogleAdminIdentity(string id)
        {
            return await service.ToogleAdminIdentity(id);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Permission(nameof(ToogleDisabled), "切换状态")]
        public async Task<UserDto> ToogleDisabled(string id)
        {
            return await service.ToogleDisabled(id);
        } 

        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<RoleViewModel>> RoleList(Pagination Pagination)
            => HttpContext.RequestServices.GetService<RoleService>().ViewModelListAsync(Pagination);

        [Permission(new string[] { "Add", "Update" })]
        [HttpPost]
        public Task<PageList<DeptViewModel>> DeptList(Pagination Pagination)
            => HttpContext.RequestServices.GetService<DeptService>().ViewModelListAsync(Pagination);
    }
}
