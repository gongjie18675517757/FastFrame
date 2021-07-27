using FastFrame.Application.Basis;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SettingController : BaseController
    {
        private readonly SettingService service;

        public SettingController(SettingService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Permission(nameof(Get), "获取设置")]
        public async Task<SettingModel> Get()
        {
            return await service.GetAsync();
        }

        [HttpPost]
        [Permission(nameof(Update), "更新设置")]
        public async Task Update(SettingModel input)
        {
            await service.UpdateAsync(input);
        }
    }
}
