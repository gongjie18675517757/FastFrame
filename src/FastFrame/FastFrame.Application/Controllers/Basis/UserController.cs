﻿using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}
