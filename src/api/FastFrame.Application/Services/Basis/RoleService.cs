using FastFrame.Dto.Basis;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastFrame.Application.Services.Basis
{
    public partial class RoleService
    {
        protected override async Task OnGeting(RoleDto dto)
        {
            await base.OnGeting(dto);
            dto.Members = await EventBus.TriggerRequestAsync<UserViewModel[], string>(dto.Id);
            dto.Permissions = await EventBus.TriggerRequestAsync<RolePermissionModel[], string>(dto.Id);
        }
    }
}
