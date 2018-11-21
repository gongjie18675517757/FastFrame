using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class RoleController
    {
        /// <summary>
        /// 设置角色成员
        /// </summary> 
        [HttpPut("{id}")]
        [Permission(nameof(SetRoleMember), "设置角色成员")]
        public async Task SetRoleMember([FromQuery]string id, [FromBody]IEnumerable<string> users)
        {
            await service.SetRoleMember(id, users);
        }

        /// <summary>
        /// 获取角色成员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Permission(AnOtherEnCodes = new string[] { nameof(SetRoleMember) })]
        public async Task<IEnumerable<UserDto>> GetRoleMember([FromQuery]string id)
        {
            return await service.GetRoleMember(id);
        }

        /// <summary>
        /// 设置角色权限
        /// </summary> 
        [HttpPut("{id}")]
        [Permission(nameof(SetRolePermission), "设置角色权限")]
        public async Task SetRolePermission([FromQuery]string id, [FromBody]IEnumerable<string> permissions)
        {
            await service.SetRolePermission(id, permissions);
        }

        /// <summary>
        /// 获取角色权限
        /// </summary> 
        [HttpGet("{id}")]
        [Permission(AnOtherEnCodes = new string[] { nameof(SetRolePermission) })]
        public async Task<IEnumerable<PermissionDto>> GetRolePermission([FromQuery]string id)
        {
            return await service.GetRolePermission(id);
        }
    }
}
