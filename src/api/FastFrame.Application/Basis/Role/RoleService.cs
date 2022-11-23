using FastFrame.Entity.Basis;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class RoleService
    {
        protected override async Task OnGeting(RoleDto dto)
        {
            await base.OnGeting(dto);
            var user_dic = await loader.GetService<RoleMemberService>().GetUserViewModelsByRoleIds(dto.Id);
            dto.Members = user_dic.SelectMany(v => v.Value);
            dto.Permissions = (await EventBus.RequestAsync<RolePermission[], RoleDto>(dto)).Select(v => v.PermissionKey);
        }
    }
}
