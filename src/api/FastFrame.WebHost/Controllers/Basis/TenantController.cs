using FastFrame.Dto.Basis;
using System.Threading.Tasks;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services.Basis;
using Microsoft.AspNetCore.Mvc;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Authorization;

namespace FastFrame.WebHost.Controllers.Basis
{
    public partial class TenantController
    {
        [EveryoneAccess]
        [HttpGet]
        public async Task<TenantDto> GetCurrent()
        {
            return await service.GetCurrentAsync();
        }


        [Permission("UpdateTenantInfo", "更新企业信息")]
        [HttpPut]
        public async Task UpdateTenantInfo([FromBody]TenantDto tenantDto)
        {
            await service.UpdateCurrentAsync(tenantDto);
        }
    }
}
