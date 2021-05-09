using FastFrame.Entity.Basis;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class RoleService
    {
        protected override async Task OnGeting(RoleDto dto)
        {
            await base.OnGeting(dto);
            dto.Members = await EventBus.RequestAsync<UserViewModel[], RoleDto>(dto);
            dto.Permissions = (await EventBus.RequestAsync<RolePermission[], RoleDto>(dto)).Select(v => v.PermissionKey);
        }
    }
}
