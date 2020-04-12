using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class UserService
    {
        private readonly ICurrentUserProvider currentUserProvider; 

        public UserService(
            ICurrentUserProvider currentUserProvider,
            IRepository<RoleMember> roleMemberRepository,
            IRepository<Role> roleRepository,
            IRepository<Resource> resourceRepository,
            IRepository<User> userRepository) : this(resourceRepository, userRepository)
        {
            this.currentUserProvider = currentUserProvider; 
        }

        protected override Task OnAdding(UserDto input, User entity)
        {
            entity.GeneratePassword();
            return base.OnAdding(input, entity);
        }

        protected override async Task OnDeleteing(User input)
        {
            await base.OnDeleteing(input);
            if (input.IsRoot)
                throw new Exception("该帐号不可以删除!");
            if (input.IsAdmin)
                throw new Exception("管理员帐号不可以删除!");
        }

        /// <summary>
        /// 切换管理员身份
        /// </summary> 
        public async Task<UserDto> ToogleAdminIdentity(string id)
        {
            if (!currentUserProvider.GetCurrUser().IsAdmin)
                throw new Exception("没有权限!");
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new Exception("用户不存在");
            user.IsAdmin = !user.IsAdmin;
            await userRepository.UpdateAsync(user);
            await userRepository.CommmitAsync();

            var cache = await RedisHelper.GetAsync<CurrUser>(id);
            if (cache != null)
            {
                cache.IsAdmin = user.IsAdmin;
                await RedisHelper.SetAsync(id, cache, 60 * 60 * 24);
            }

            return await GetAsync(id);
        }

        /// <summary>
        /// 切换禁用状态
        /// </summary> 
        public async Task<UserDto> ToogleDisabled(string id)
        {
            if (!currentUserProvider.GetCurrUser().IsAdmin)
                throw new Exception("没有权限!");
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new Exception("用户不存在");
            user.IsDisabled = !user.IsDisabled;
            await userRepository.UpdateAsync(user);
            await userRepository.CommmitAsync();

            return await GetAsync(id);
        }

        protected override async Task OnGeting(UserDto dto)
        {
            await base.OnGeting(dto);
            dto.Roles = await EventBus.TriggerRequestAsync<RoleViewModel[], string>(dto.Id);
            dto.Depts = await EventBus.TriggerRequestAsync<DeptViewModel[], string>(dto.Id);
        }

        protected override async Task OnGetListing(IEnumerable<UserDto> dtos)
        {
            await base.OnGetListing(dtos);
            var keys = dtos.Select(v => v.Id).ToArray();

            var roleMaps = await EventBus.TriggerRequestAsync<IEnumerable<KeyValuePair<string, RoleViewModel[]>>, string[]>(keys);
            var deptMaps = await EventBus.TriggerRequestAsync<IEnumerable<KeyValuePair<string, DeptViewModel[]>>, string[]>(keys);
            foreach (var item in dtos)
            {
                item.Roles = roleMaps.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
                item.Depts = deptMaps.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
            }
        } 
    }
}
