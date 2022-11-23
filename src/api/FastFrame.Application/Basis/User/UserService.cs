﻿using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Database.Mapping.Basis;

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
            var role_dic = await loader.GetService<RoleMemberService>().GetRoleViewModelsByUserIds(dto.Id);
            dto.Roles = role_dic.SelectMany(v => v.Value);

            var dept_dic = await loader.GetService<DeptMemberService>().GetDeptViewModelsByUserIds(dto.Id);
            dto.Depts = dept_dic.SelectMany(v => v.Value);
        }

        protected override async Task OnGetListing(IEnumerable<UserDto> dtos)
        {
            await base.OnGetListing(dtos);

            var keys = dtos.Select(v => v.Id).ToArray();
            var role_dic = await loader.GetService<RoleMemberService>().GetRoleViewModelsByUserIds(keys);
            var dept_dic = await loader.GetService<DeptMemberService>().GetDeptViewModelsByUserIds(keys);

            foreach (var item in dtos)
            {
                item.Roles = role_dic.TryGetValueOrDefault(item.Id);
                item.Depts = dept_dic.TryGetValueOrDefault(item.Id);
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

        protected override IQueryable<UserDto> GetListQueryableing(IQueryable<UserDto> query, IPagination<UserDto> pageInfo)
        {
            query = base.GetListQueryableing(query, pageInfo);

            pageInfo
                .Filters
                .TryReplaceQueryFilters<FieldQueryFilter<UserDto>>(
                    v => string.Compare(v.Name, nameof(ITreeModel.Super_Id), true) == 0,
                    HandleSuperIdFilterQuery);

            return query;
        }

        /// <summary>
        /// 处理查询参数:Super_Id
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private IQueryFilter<UserDto> HandleSuperIdFilterQuery(FieldQueryFilter<UserDto> v)
        {
            var super_id = v.Value;
            var depts = loader.GetService<IRepository<Dept>>();
            var treeChildren = depts.Where(v => depts.Any(y => y.Id == super_id && v.TreeCode.StartsWith(y.TreeCode)));

            var deptMembers = loader.GetService<IRepository<DeptMember>>().Where(v => v.Dept_Id == super_id || treeChildren.Any(x => x.Id == v.Dept_Id));

            return new ExpressionQueryFilter<UserDto>(v => deptMembers.Any(x => x.User_Id == v.Id));
        }
    }
}
