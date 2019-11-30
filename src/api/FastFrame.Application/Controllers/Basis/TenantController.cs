using FastFrame.Dto.Basis;
using System.Threading.Tasks;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Authorization;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class TenantController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<TenantDto> GetCurrent()
        {
            return await service.GetCurrentAsync();
        }


        [Permission("UpdateTenantInfo", "更新企业信息")]
        [HttpPut]
        public async Task<TenantDto> UpdateTenantInfo([FromBody]TenantDto tenantDto)
        {
            return await service.UpdateCurrentAsync(tenantDto);
        }
    }
}
