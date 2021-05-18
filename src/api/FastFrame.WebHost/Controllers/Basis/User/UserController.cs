using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
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
        [HttpGet]
        public Task<PageList<RoleViewModel>> RoleList(string qs)
            => HttpContext.RequestServices.GetService<RoleService>().ViewModelListAsync(qs.ToObject<Pagination>(true));

        [Permission(new string[] { "Add", "Update" })]
        [HttpGet]
        public Task<PageList<DeptViewModel>> DeptList(string qs)
            => HttpContext.RequestServices.GetService<DeptService>().ViewModelListAsync(qs.ToObject<Pagination>(true));
    }
}
