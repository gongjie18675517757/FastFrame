using FastFrame.Application.Basis;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
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
        public async Task UpdateTenantInfo([FromBody]TenantDto tenantDto)
        {
            await service.UpdateCurrentAsync(tenantDto);
        }
    }
}
