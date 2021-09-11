﻿using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Application.Basis
{
    public partial class UserService
    {
        protected override Task OnAdding(UserDto input, User entity)
        {
            entity.GeneratePassword();
            return base.OnAdding(input, entity);
        }

        protected override async Task OnDeleteing(User input)
        {
            await base.OnDeleteing(input);
            if (input.IsAdmin)
                throw new MsgException("管理员帐号不可以删除!请先移除管理员身份");

            /*判断该帐号是否有操作记录，如果有则不允许删除*/
            /*待实现*/
        }

        /// <summary>
        /// 切换管理员身份
        /// </summary> 
        public async Task<UserDto> ToogleAdminIdentity(string id)
        {
            if (!AppSession.CurrUser.IsAdmin)
                throw new MsgException("管理员才可以做此操作!");
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new MsgException("用户不存在");
            user.IsAdmin = !user.IsAdmin;
            await userRepository.UpdateAsync(user);
            await userRepository.CommmitAsync();

            return await GetAsync(id);
        }

        /// <summary>
        /// 切换禁用状态
        /// </summary> 
        public async Task<UserDto> ToogleDisabled(string id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new MsgException("用户不存在");
            switch (user.Enable)
            {
                case Entity.Enums.EnabledMark.enabled:
                    user.Enable = Entity.Enums.EnabledMark.disabled;
                    break;
                case Entity.Enums.EnabledMark.disabled:
                    user.Enable = Entity.Enums.EnabledMark.enabled;
                    break;
                default:
                    break;
            }
            await userRepository.UpdateAsync(user);
            await userRepository.CommmitAsync();

            return await GetAsync(id);
        }

        protected override async Task OnGeting(UserDto dto)
        {
            await base.OnGeting(dto);
            dto.Roles = await EventBus.RequestAsync<RoleViewModel[], UserDto>(dto);
            dto.Depts = await EventBus.RequestAsync<DeptViewModel[], UserDto>(dto);
        }

        protected override async Task OnGetListing(IEnumerable<UserDto> dtos)
        {
            await base.OnGetListing(dtos);

            var roleMaps = await EventBus.RequestAsync<IEnumerable<KeyValuePair<string, RoleViewModel[]>>, UserDto[]>(dtos.ToArray());
            var deptMaps = await EventBus.RequestAsync<IEnumerable<KeyValuePair<string, DeptViewModel[]>>, UserDto[]>(dtos.ToArray());
            foreach (var item in dtos)
            {
                item.Roles = roleMaps.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
                item.Depts = deptMaps.Where(v => v.Key == item.Id).SelectMany(v => v.Value);
            }
        }

        protected override Task OnBeforeUpdate(User before, UserDto after)
        {
            if (before.Password != after.Password)
            {
                before.Password = after.Password;
                before.GeneratePassword();

                after.Password = before.Password;
            }

            return base.OnBeforeUpdate(before, after);
        }

        protected override IQueryable<UserDto> GetListQueryableing(IQueryable<UserDto> query, Pagination pageInfo)
        {
            query = base.GetListQueryableing(query, pageInfo);
            if (pageInfo.TryGetFilterValue(v => v.Name == nameof(ITreeModel.Super_Id), out var superId) && !superId.IsNullOrWhiteSpace())
            {
                var treeChildren = Loader.GetService<IRepository<TreeChild>>().Where(v => v.Super_Id == superId);
                var deptMembers = Loader.GetService<IRepository<DeptMember>>().Where(v => v.Dept_Id == superId || treeChildren.Any(x => x.Child_Id == v.Dept_Id));

                query = query.Where(v => deptMembers.Any(x => x.User_Id == v.Id));
            }
            return query;
        }
    }
}
