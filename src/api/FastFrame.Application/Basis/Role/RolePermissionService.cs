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
        IRequestHandle<RolePermission[], RoleDto>
    {
        private readonly IRepository<RolePermission> rolePermissions;
        private readonly HandleOne2ManyService<string, RolePermission> handlePermissionService;
   

        public RolePermissionService(
            IRepository<RolePermission> rolePermissions,
            HandleOne2ManyService<string, RolePermission> handlePermissionService )
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
                    (a, b) => a.PermissionKey == b,
                    v => new RolePermission
                    {
                        Role_Id = @event.Data.Id,
                        PermissionKey = v, 
                    });
        }

        public async Task<RolePermission[]> HandleRequestAsync(RoleDto request)
        {
            return await rolePermissions
                .Where(v => v.Role_Id == request.Id) 
                .ToArrayAsync();
        }


        public async Task HandleEventAsync(DoMainAdding<RoleDto> @event)
        {
            var input = @event.Data;
            await handlePermissionService
               .AddManyAsync(input.Permissions, v => new RolePermission
               {
                   Role_Id = input.Id,
                   PermissionKey = v, 
               });
        }
    }
}
