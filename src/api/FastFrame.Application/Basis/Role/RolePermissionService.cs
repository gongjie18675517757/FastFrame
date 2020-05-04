using FastFrame.Application.Events;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class RolePermissionService : IService,
        IEventHandle<DoMainAdding<RoleDto>>,
        IEventHandle<DoMainDeleteing<RoleDto>>,
        IEventHandle<DoMainUpdateing<RoleDto>>,
        IRequestHandle<RolePermissionModel[], RoleDto>
    {
        private readonly IRepository<RolePermission> rolePermissions;
        private readonly HandleOne2ManyService<RolePermissionModel, RolePermission> handlePermissionService;
   

        public RolePermissionService(
            IRepository<RolePermission> rolePermissions,
            HandleOne2ManyService<RolePermissionModel, RolePermission> handlePermissionService )
        {
            this.rolePermissions = rolePermissions;
            this.handlePermissionService = handlePermissionService;
   
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
                    (a, b) => a.PermissionKey == b.PermissionKey && a.SuperPermissionKey == b.SuperPermissionKey,
                    v => new RolePermission
                    {
                        Role_Id = @event.Data.Id,
                        PermissionKey = v.PermissionKey,
                        SuperPermissionKey = v.SuperPermissionKey
                    });
        }

        public async Task<RolePermissionModel[]> HandleRequestAsync(RoleDto request)
        {
            return await rolePermissions
                .Where(v => v.Role_Id == request.Id)
                .MapTo<RolePermission, RolePermissionModel>()
                .ToArrayAsync();
        }


        public async Task HandleEventAsync(DoMainAdding<RoleDto> @event)
        {
            var input = @event.Data;
            await handlePermissionService
               .AddManyAsync(input.Permissions, v => new RolePermission
               {
                   Role_Id = input.Id,
                   PermissionKey = v.PermissionKey,
                   SuperPermissionKey = v.SuperPermissionKey
               });
        }
    }
}
