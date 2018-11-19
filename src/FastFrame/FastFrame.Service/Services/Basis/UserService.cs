using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using FastFrame.Repository.Basis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class UserService
    {
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly RoleMemberRepository roleMemberRepository;
        private readonly RolePermissionRepository rolePermissionRepository;

        public UserService(ICurrentUserProvider currentUserProvider,
            RoleMemberRepository roleMemberRepository,           
            ForeignRepository foreignRepository,
            UserRepository userRepository, IScopeServiceLoader loader) : this(foreignRepository, userRepository, loader)
        {
            this.currentUserProvider = currentUserProvider;
            this.roleMemberRepository = roleMemberRepository;
         
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
        /// <param name="id"></param>
        /// <returns></returns>
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
            return await GetAsync(id);
        }

        /// <summary>
        /// 切换禁用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 设置用户角色
        /// </summary>         
        public async Task SetUserRoles(string id, IEnumerable<RoleDto> roles)
        {
            var before = await roleMemberRepository.Queryable.Where(x => x.User_Id == id).ToListAsync();
            var comparisonCollection = new ComparisonCollection<RoleMember, RoleDto>(before, roles, (a, b) => a.Role_Id == b.Id);
            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                await roleMemberRepository.AddAsync(new RoleMember()
                {
                    Role_Id = item.Id,
                    User_Id = id
                });
            }

            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await roleMemberRepository.DeleteAsync(item);
            }

            await roleMemberRepository.CommmitAsync();
        } 
    }
}
