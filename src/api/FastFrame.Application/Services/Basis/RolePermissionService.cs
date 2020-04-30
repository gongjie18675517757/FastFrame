using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using FastFrame.Application.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Services.Basis
{
    public partial class RolePermissionService : IService,
        IEventHandle<DoMainAdding<RoleDto>>,
        IEventHandle<DoMainDeleteing<RoleDto>>,
        IEventHandle<DoMainUpdateing<RoleDto>>,
        IRequestHandle<RolePermissionModel[], string>
    {
        private readonly IRepository<RolePermission> rolePermissions;
        private readonly HandleOne2ManyService<RolePermissionModel, RolePermission> handlePermissionService;
        private readonly IRepository<Permission> permissions;

        public RolePermissionService(
            IRepository<RolePermission> rolePermissions,
            HandleOne2ManyService<RolePermissionModel, RolePermission> handlePermissionService,
            IRepository<Permission> permissions)
        {
            this.rolePermissions = rolePermissions;
            this.handlePermissionService = handlePermissionService;
            this.permissions = permissions;
        }

        public async Task HandleEventAsync(DoMainDeleteing<RoleDto> @event)
        {

            await handlePermissionService.DelManyAsync(v => v.Role_Id == @event.Id);
        }

        public async Task HandleEventAsync(DoMainUpdateing<RoleDto> @event)
        {
            await handlePermissionService.UpdateManyAsync(
                    v => v.Role_Id == @event.Data.Id,
                    @event.Data.Permissions.SelectMany(v => v.Children.Where(r => r.IsAuthorization)),
                    (a, b) => a.Role_Id == b.Id,
                    v => new RolePermission
                    {
                        Role_Id = @event.Data.Id,
                        Permission_Id = v.Id
                    });
        }

        public async Task<RolePermissionModel[]> HandleRequestAsync(string request)
        {
            var query = from a in permissions
                        select new RolePermissionModel
                        {
                            Id = a.Id,
                            EnCode = a.EnCode,
                            Name = a.Name,
                            Super_Id = a.Super_Id,
                            IsAuthorization = rolePermissions
                                                 .Any(r => r.Role_Id == request &&
                                                             (r.Permission_Id == a.Id))
                        };

            var list = await query.ToListAsync();

            return LoadTree(list, null).ToArray();
        }

        public async Task<RolePermissionModel[]> GetPermissionModelListAsync()
        {
            var query = from a in permissions
                        select new RolePermissionModel
                        {
                            Id = a.Id,
                            EnCode = a.EnCode,
                            Name = a.Name,
                            Super_Id = a.Super_Id,
                            IsAuthorization = false
                        };

            var list = await query.ToListAsync();

            return LoadTree(list, null).ToArray();
        }

        private IEnumerable<RolePermissionModel> LoadTree(IEnumerable<RolePermissionModel> rolePermissionModels, string key)
        {
            var children = rolePermissionModels.Where(v => v.Super_Id == key);
            foreach (var item in children)
            {
                item.Children = LoadTree(rolePermissionModels, item.Id);
                yield return item;
            }
        }

        public async Task HandleEventAsync(DoMainAdding<RoleDto> @event)
        {
            var input = @event.Data;
            await handlePermissionService
               .AddManyAsync(input.Permissions.SelectMany(v => v.Children.Where(r => r.IsAuthorization)), v => new RolePermission
               {
                   Role_Id = input.Id,
                   Permission_Id = v.Id
               });
        }
    }
}
