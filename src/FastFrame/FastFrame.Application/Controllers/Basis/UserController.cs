using FastFrame.Dto.Basis;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class UserController
    {
        /// <summary>
        /// 切换管理员身份
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<UserDto> ToogleAdminIdentity(string id)
        {
            return await service.ToogleAdminIdentity(id);
        }
        /// <summary>
        /// 切换禁用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<UserDto> ToogleDisabled(string id)
        {
            return await service.ToogleDisabled(id);
        }
    }
}
