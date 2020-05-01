using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class RoleService
    {
        protected override async Task OnGeting(RoleDto dto)
        {
            await base.OnGeting(dto);
            dto.Members = await EventBus.RequestAsync<UserViewModel[], string>(dto.Id);
            dto.Permissions = await EventBus.RequestAsync<RolePermissionModel[], string>(dto.Id);
        }
    }
}
