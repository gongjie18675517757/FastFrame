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
        public async Task SetRoleMember([FromQuery]string id, [FromBody]IEnumerable<UserDto> users)
        {
            await service.SetRoleMember(id, users);
        }

        /// <summary>
        /// 设置角色权限
        /// </summary> 
        [HttpPut("{id}")]
        [Permission(nameof(SetRolePermission), "设置角色权限")]
        public async Task SetRolePermission([FromQuery]string id, [FromBody]IEnumerable<PermissionDto> permissions)
        {
            await service.SetRolePermission(id, permissions);
        } 
    }
}
