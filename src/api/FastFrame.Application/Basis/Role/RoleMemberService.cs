using FastFrame.Application.Events;
using FastFrame.Entity.Basis;
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
        IRequestHandle<UserViewModel[], string>,

        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>,
        IRequestHandle<RoleViewModel[], string>,
        IRequestHandle<IEnumerable<KeyValuePair<string, RoleViewModel[]>>, string[]>
    {
        private readonly IRepository<RoleMember> roleMembers;
        private readonly IRepository<User> users;
        private readonly IRepository<Role> roles;
        private readonly HandleOne2ManyService<UserViewModel, RoleMember> handleRoleMemberService;
        private readonly HandleOne2ManyService<RoleViewModel, RoleMember> handleUserRoleService;

        public RoleMemberService(
            IRepository<RoleMember> roleMembers,
            IRepository<User> users,
            IRepository<Role> roles,
            HandleOne2ManyService<UserViewModel, RoleMember> handleRoleMemberService,
            HandleOne2ManyService<RoleViewModel, RoleMember> handleUserRoleService)
        {
            this.roleMembers = roleMembers;
            this.users = users;
            this.roles = roles;
            this.handleRoleMemberService = handleRoleMemberService;
            this.handleUserRoleService = handleUserRoleService;
        }

        public async Task HandleEventAsync(DoMainDeleteing<RoleDto> @event)
        {
            await handleRoleMemberService.DelManyAsync(v => v.Role_Id == @event.Id);
        }

        public async Task HandleEventAsync(DoMainUpdateing<RoleDto> @event)
        {
            await handleRoleMemberService.UpdateManyAsync(
                    v => v.Role_Id == @event.Data.Id,
                    @event.Data.Members,
                    (a, b) => a.Role_Id == b.Id,
                    v => new RoleMember
                    {
                        Role_Id = @event.Data.Id,
                        User_Id = v.Id
                    });
        }

        public Task<UserViewModel[]> HandleRequestAsync(string request)
        {
            return users.Where(v => roleMembers.Any(r => r.User_Id == v.Id && r.Role_Id == request))
                          .Select(v => new UserViewModel
                          {
                              Account = v.Account,
                              Id = v.Id,
                              Name = v.Name
                          })
                          .ToArrayAsync();
        }

        public async Task HandleEventAsync(DoMainAdding<RoleDto> @event)
        {
            var input = @event.Data;
            await handleRoleMemberService
                .AddManyAsync(input.Members, v => new RoleMember
                {
                    Role_Id = input.Id,
                    User_Id = v.Id
                });
        }

        public async Task HandleEventAsync(DoMainAdding<UserDto> @event)
        {
            await handleUserRoleService.AddManyAsync(@event.Data.Roles, v => new RoleMember
            {
                Role_Id = v.Id,
                User_Id = @event.Data.Id
            });
        }

        public async Task HandleEventAsync(DoMainDeleteing<UserDto> @event)
        {
            await handleUserRoleService.DelManyAsync(v => v.User_Id == @event.Id);
        }

        public async Task HandleEventAsync(DoMainUpdateing<UserDto> @event)
        {
            await handleUserRoleService.UpdateManyAsync(
                v => v.User_Id == @event.Data.Id,
                @event.Data.Roles,
                (a, b) => a.User_Id == b.Id,
                v => new RoleMember
                {
                    Role_Id = v.Id,
                    User_Id = @event.Data.Id
                });
        } 

        Task<RoleViewModel[]> IRequestHandle<RoleViewModel[], string>.HandleRequestAsync(string request)
        {
            return roles
                     .Where(v => roleMembers.Any(r => r.Role_Id == v.Id && r.User_Id == request))
                     .Select(v => new RoleViewModel { EnCode = v.EnCode, Id = v.Id, Name = v.Name })
                     .ToArrayAsync();
        }

        public async Task<IEnumerable<KeyValuePair<string, RoleViewModel[]>>> HandleRequestAsync(string[] request)
        {
            var query = from a in roles
                        join b in roleMembers on a.Id equals b.Role_Id
                        where request.Contains(b.User_Id)
                        select new
                        {
                            b.User_Id,
                            Role = new RoleViewModel
                            {
                                Name = a.Name,
                                EnCode = a.EnCode,
                                Id = a.Id
                            }
                        };

            return (await query.ToListAsync()).GroupBy(v => v.User_Id)
                        .Select(v => new KeyValuePair<string, RoleViewModel[]>(v.Key, v.Select(r => r.Role).ToArray()));
        }
    }
}
