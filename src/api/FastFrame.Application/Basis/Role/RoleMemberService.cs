﻿using FastFrame.Application.Events;
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
        IRequestHandle<UserViewModel[], RoleDto>,

        IEventHandle<DoMainAdding<UserDto>>,
        IEventHandle<DoMainDeleteing<UserDto>>,
        IEventHandle<DoMainUpdateing<UserDto>>,
        IRequestHandle<RoleViewModel[], UserDto>,
        IRequestHandle<IEnumerable<KeyValuePair<string, RoleViewModel[]>>, UserDto[]>
    {
        private readonly IRepository<RoleMember> roleMembers;
        private readonly IRepository<User> users;
        private readonly IRepository<Role> roles;
        private readonly IRepository<FlowStepUser> flowStepUsers;
        private readonly IRepository<FlowInstance> flowInstances;
        private readonly HandleOne2ManyService<UserViewModel, RoleMember> handleRoleMemberService;
        private readonly HandleOne2ManyService<RoleViewModel, RoleMember> handleUserRoleService;

        public RoleMemberService(
            IRepository<RoleMember> roleMembers,
            IRepository<User> users,
            IRepository<Role> roles,
            IRepository<FlowStepUser> flowStepUsers,
            IRepository<FlowInstance> flowInstances,
            HandleOne2ManyService<UserViewModel, RoleMember> handleRoleMemberService,
            HandleOne2ManyService<RoleViewModel, RoleMember> handleUserRoleService)
        {
            this.roleMembers = roleMembers;
            this.users = users;
            this.roles = roles;
            this.flowStepUsers = flowStepUsers;
            this.flowInstances = flowInstances;
            this.handleRoleMemberService = handleRoleMemberService;
            this.handleUserRoleService = handleUserRoleService;
        }

        public async Task HandleEventAsync(DoMainDeleteing<RoleDto> @event)
        {
            await handleRoleMemberService.DelManyAsync(v => v.Role_Id == @event.Id);

            /*处理流程中此角色的审批人*/
            var stepUsers = await flowStepUsers
                    .Where(v => v.BeRole_Id == @event.Id && flowInstances.Any(r => r.Id == v.FlowInstance_Id))
                    .ToListAsync();

            foreach (var stepUser in stepUsers)
            {
                await flowStepUsers.DeleteAsync(stepUser);
            }
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

            /*处理流程中此角色的审批人*/

        }

        public Task<UserViewModel[]> HandleRequestAsync(RoleDto request)
        {
            var userQuery = users.MapTo<User, UserViewModel>();
            return userQuery.Where(v => roleMembers.Any(r => r.User_Id == v.Id && r.Role_Id == request.Id))
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

            /*处理流程中此角色的审批人*/
        }

        public async Task HandleEventAsync(DoMainUpdateing<UserDto> @event)
        {
            await handleUserRoleService.UpdateManyAsync(
                v => v.User_Id == @event.Data.Id,
                @event.Data.Roles,
                (a, b) => a.Role_Id == b.Id,
                v => new RoleMember
                {
                    Role_Id = v.Id,
                    User_Id = @event.Data.Id
                });

            /*处理流程中此角色的审批人*/

        }

        Task<RoleViewModel[]> IRequestHandle<RoleViewModel[], UserDto>.HandleRequestAsync(UserDto request)
        {
            return roles
                     .Where(v => roleMembers.Any(r => r.Role_Id == v.Id && r.User_Id == request.Id))
                     .MapTo<Role, RoleViewModel>()
                     .ToArrayAsync();
        }

        public async Task<IEnumerable<KeyValuePair<string, RoleViewModel[]>>> HandleRequestAsync(UserDto[] request)
        {
            var roleQuery = roles.MapTo<Role, RoleViewModel>();
            var keys = request.Select(v => v.Id).ToArray();
            var query = from a in roleQuery
                        join b in roleMembers on a.Id equals b.Role_Id
                        where keys.Contains(b.User_Id)
                        select new
                        {
                            b.User_Id,
                            Role = a
                        };

            return (await query.ToListAsync()).GroupBy(v => v.User_Id)
                        .Select(v => new KeyValuePair<string, RoleViewModel[]>(v.Key, v.Select(r => r.Role).ToArray()));
        }
    }
}
