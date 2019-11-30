using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using FastFrame.Service.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class RoleMemberService : IService,
        IEventHandle<DoMainAdding<RoleDto>>,
        IEventHandle<DoMainDeleteing<RoleDto>>,
        IEventHandle<DoMainUpdateing<RoleDto>>,
        IEventHandle<DoMainResulting<RoleDto>>,
        IEventHandle<DoMainResultListing<RoleDto>>,

        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>,
        IEventHandle<DoMainResulting<UserDto>>,
        IEventHandle<DoMainResultListing<UserDto>>
    {
        private readonly IRepository<RoleMember> roleMembers;
        private readonly UserService userService;
        private readonly RoleService roleService;
        private readonly HandleOne2ManyService<UserDto, RoleMember> handleRoleMemberService;
        private readonly HandleOne2ManyService<RoleDto, RoleMember> handleUserRoleService;

        public RoleMemberService(
            IRepository<RoleMember> roleMembers,
            UserService userService,
            RoleService roleService,
            HandleOne2ManyService<UserDto, RoleMember> handleRoleMemberService,
            HandleOne2ManyService<RoleDto, RoleMember> handleUserRoleService)
        {
            this.roleMembers = roleMembers;
            this.userService = userService;
            this.roleService = roleService;
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

        public async Task HandleEventAsync(DoMainResulting<RoleDto> @event)
        {
            @event.Data.Members = await userService
                           .Query()
                           .Where(v =>
                                   roleMembers.Any(r => r.User_Id == v.Id && r.Role_Id == @event.Data.Id)
                                  )
                            .ToListAsync();
        }

        public async Task HandleEventAsync(DoMainResultListing<RoleDto> @event)
        {
            var keys = @event.Data.Select(v => v.Id).ToList();
            var query = from a in userService.Query()
                        join b in roleMembers on a.Id equals b.User_Id
                        where keys.Contains(b.Role_Id)
                        select new
                        {
                            Item = a,
                            RoleId = b.Role_Id
                        };
            var list = await query.ToListAsync();

            foreach (var item in @event.Data)
            {
                item.Members = list.Where(v => v.RoleId == item.Id).Select(v => v.Item);
            }
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

        public async Task HandleEventAsync(DoMainResulting<UserDto> @event)
        {
            @event.Data.Roles = await roleService
                          .Query()
                          .Where(v =>
                                  roleMembers.Any(r => r.Role_Id == v.Id && r.User_Id == @event.Data.Id)
                                 )
                           .ToListAsync();
        }

        public async Task HandleEventAsync(DoMainResultListing<UserDto> @event)
        {
            var keys = @event.Data.Select(v => v.Id).ToList();
            var query = from a in roleService.Query()
                        join b in roleMembers on a.Id equals b.Role_Id
                        where keys.Contains(b.User_Id)
                        select new
                        {
                            Item = a,
                            UserId = b.User_Id
                        };
            var list = await query.ToListAsync();

            foreach (var item in @event.Data)
            {
                item.Roles = list.Where(v => v.UserId == item.Id).Select(v => v.Item);
            }
        }
    }
}
