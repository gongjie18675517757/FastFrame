using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using FastFrame.Service.Events;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class RolePermissionService : IService,
        IEventHandle<DoMainAdding<RoleDto>>,
        IEventHandle<DoMainDeleteing<RoleDto>>,
        IEventHandle<DoMainUpdateing<RoleDto>>,
        IEventHandle<DoMainResulting<RoleDto>>,
        IEventHandle<DoMainResultListing<RoleDto>>
    {
        private readonly IRepository<RolePermission> rolePermissions;
        private readonly HandleOne2ManyService<PermissionDto, RolePermission> handlePermissionService;
        private readonly PermissionService permissionService;

        public RolePermissionService(
            IRepository<RolePermission> rolePermissions,
            HandleOne2ManyService<PermissionDto, RolePermission> handlePermissionService,
            PermissionService permissionService)
        {
            this.rolePermissions = rolePermissions;
            this.handlePermissionService = handlePermissionService;
            this.permissionService = permissionService;
        }

        public async Task HandleEventAsync(DoMainDeleteing<RoleDto> @event)
        {

            await handlePermissionService.DelManyAsync(v => v.Role_Id == @event.Id);
        }

        public async Task HandleEventAsync(DoMainUpdateing<RoleDto> @event)
        {
            await handlePermissionService.UpdateManyAsync(
                    v => v.Role_Id == @event.Data.Id,
                    @event.Data.Permissions,
                    (a, b) => a.Role_Id == b.Id,
                    v => new RolePermission
                    {
                        Role_Id = @event.Data.Id,
                        Permission_Id = v.Id
                    });
        }

        public async Task HandleEventAsync(DoMainResulting<RoleDto> @event)
        {
            @event.Data.Permissions = await permissionService
                            .Query()
                            .Where(v =>
                                    rolePermissions.Any(r => r.Permission_Id == v.Id && r.Role_Id == @event.Data.Id)
                                   )
                             .ToListAsync();
        }

        public async Task HandleEventAsync(DoMainResultListing<RoleDto> @event)
        {
            //var keys = @event.Data.Select(v => v.Id).ToList();
            //var query = from a in permissionService.Query()
            //            join b in rolePermissions on a.Id equals b.Permission_Id
            //            where keys.Contains(b.Role_Id)
            //            select new
            //            {
            //                Item = a,
            //                RoleId = b.Role_Id
            //            };
            //var list = await query.ToListAsync();

            //foreach (var item in @event.Data)
            //{
            //    item.Permissions = list.Where(v => v.RoleId == item.Id).Select(v => v.Item);
            //}
            await Task.CompletedTask;
        }

        public async Task HandleEventAsync(DoMainAdding<RoleDto> @event)
        {
            var input = @event.Data;
            await handlePermissionService
               .AddManyAsync(input.Permissions, v => new RolePermission
               {
                   Role_Id = input.Id,
                   Permission_Id = v.Id
               });
        }
    }
}
