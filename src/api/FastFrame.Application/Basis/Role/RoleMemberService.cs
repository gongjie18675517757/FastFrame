using FastFrame.Application.Events;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class RoleMemberService : IService,
        IEventHandle<DoMainAdding<RoleDto>>,
        IEventHandle<DoMainDeleteing<RoleDto>>,
        IEventHandle<DoMainUpdateing<RoleDto>>,

        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>
    {
        private readonly IRepository<RoleMember> roleMembers;
        private readonly IRepository<User> users;
        private readonly IRepository<Role> roles;
        private readonly HandleOne2ManyService<IViewModel, RoleMember> handleRoleMemberService;
        private readonly HandleOne2ManyService<IViewModel, RoleMember> handleUserRoleService;

        public RoleMemberService(
            IRepository<RoleMember> roleMembers,
            IRepository<User> users,
            IRepository<Role> roles,
            HandleOne2ManyService<IViewModel, RoleMember> handleRoleMemberService,
            HandleOne2ManyService<IViewModel, RoleMember> handleUserRoleService)
        {
            this.roleMembers = roleMembers;
            this.users = users;
            this.roles = roles;
            this.handleRoleMemberService = handleRoleMemberService;
            this.handleUserRoleService = handleUserRoleService;
        }

        public async Task HandleEventAsync(DoMainDeleteing<RoleDto> @event)
        {
            await handleRoleMemberService.DelManyAsync(v => v.FKey_Id == @event.Id);

        }

        public async Task HandleEventAsync(DoMainUpdateing<RoleDto> @event)
        {
            await handleRoleMemberService.UpdateManyAsync(
                    v => v.FKey_Id == @event.Data.Id,
                    @event.Data.Members,
                    (a, b) => a.FKey_Id == b.Id,
                    v => new RoleMember
                    {
                        FKey_Id = @event.Data.Id,
                        Value_Id = v.Id
                    });

            /*处理流程中此角色的审批人*/

        }



        public async Task HandleEventAsync(DoMainAdding<RoleDto> @event)
        {
            var input = @event.Data;
            await handleRoleMemberService
                .AddManyAsync(input.Members, v => new RoleMember
                {
                    FKey_Id = input.Id,
                    Value_Id = v.Id
                });
        }

        public async Task HandleEventAsync(DoMainAdding<UserDto> @event)
        {
            await handleUserRoleService.AddManyAsync(@event.Data.Roles, v => new RoleMember
            {
                FKey_Id = v.Id,
                Value_Id = @event.Data.Id
            });
        }

        public async Task HandleEventAsync(DoMainDeleteing<UserDto> @event)
        {
            await handleUserRoleService.DelManyAsync(v => v.Value_Id == @event.Id);

            /*处理流程中此角色的审批人*/
        }

        public async Task HandleEventAsync(DoMainUpdateing<UserDto> @event)
        {
            await handleUserRoleService.UpdateManyAsync(
                v => v.Value_Id == @event.Data.Id,
                @event.Data.Roles,
                (a, b) => a.FKey_Id == b.Id,
                v => new RoleMember
                {
                    FKey_Id = v.Id,
                    Value_Id = @event.Data.Id
                });

            /*处理流程中此角色的审批人*/

        }

        public async Task<Dictionary<string, IEnumerable<IViewModel>>> GetUserViewModelsByRoleIds(params string[] keys)
        {
            var userQuery = users.Select(User.BuildExpression());
            var query = from a in userQuery
                        join b in roleMembers on a.Id equals b.Value_Id
                        where keys.Contains(b.FKey_Id)
                        select new
                        {
                            vm = a,
                            b.FKey_Id
                        };
            var list = await query.ToListAsync();

            return keys
                .Where(v => !v.IsNullOrWhiteSpace())
                .Distinct()
                .ToDictionary(
                    v => v,
                    v => list.Where(x => x.FKey_Id == v).Select(x => x.vm)
                );
        }

        public async Task<Dictionary<string, IEnumerable<IViewModel>>> GetRoleViewModelsByUserIds(params string[] keys)
        {
            var roleQuery = roles.Select(Role.BuildExpression());
            var query = from a in roleQuery
                        join b in roleMembers on a.Id equals b.Value_Id
                        where keys.Contains(b.Value_Id)
                        select new
                        {
                            vm = a,
                            b.Value_Id
                        };

            var list = await query.ToListAsync();

            return keys
                .Where(v => !v.IsNullOrWhiteSpace())
                .Distinct()
                .ToDictionary(
                    v => v,
                    v => list.Where(x => x.Value_Id == v).Select(x => x.vm)
                );
        }
    }
}
