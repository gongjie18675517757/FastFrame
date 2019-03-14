using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class PermissionService: IEqualityComparer<Permission>
    {
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<RoleMember> roleMemberRepository;
        private readonly IRepository<RolePermission> rolePermissionRepository;
        private readonly ICurrentUserProvider currentUserProvider;

        public PermissionService(
            IRepository<Permission> permissionRepository,
            IRepository<User> userRepository,
            IRepository<Role> roleRepository,
            IRepository<RoleMember> roleMemberRepository,
            IRepository<RolePermission> rolePermissionRepository,
            ICurrentUserProvider currentUserProvider,
            IScopeServiceLoader loader)
            : this(permissionRepository, userRepository, loader)
        {
            this.roleRepository = roleRepository;
            this.roleMemberRepository = roleMemberRepository;
            this.rolePermissionRepository = rolePermissionRepository;
            this.currentUserProvider = currentUserProvider;
        }
        /// <summary>
        /// 初始化权限
        /// </summary>        
        public async Task InitPermission(IEnumerable<PermissionDto> permissionDtos)
        {
            var beforeEntitys = await permissionRepository.Queryable.ToListAsync();
            var comparisonCollection = new ComparisonCollection<Permission, PermissionDto>(beforeEntitys, permissionDtos,
                (a, b) => a.EnCode == b.EnCode && a.Parent_Id == b.Parent_Id);
            foreach (var item in comparisonCollection.GetCollectionByAdded().ToList())
            {
                var entity = item.MapTo<PermissionDto, Permission>();
                entity = await permissionRepository.AddAsync(entity);
                item.Id = entity.Id;
                foreach (var child in item.Permissions)
                    child.Parent_Id = entity.Id;
            }

            foreach (var (before, after) in comparisonCollection.GetCollectionByModify())
            {
                after.Id = before.Id;
                before.AreaName = after.AreaName;
                foreach (var child in after.Permissions)
                    child.Parent_Id = before.Id;
            }
            beforeEntitys = comparisonCollection.GetCollectionByDeleted().ToList();
            comparisonCollection = new ComparisonCollection<Permission, PermissionDto>(
                    beforeEntitys,
                    permissionDtos.SelectMany(x => x.Permissions),
                    (a, b) => a.EnCode == b.EnCode && a.Parent_Id == b.Parent_Id);

            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                var entity = item.MapTo<PermissionDto, Permission>();
                entity = await permissionRepository.AddAsync(entity);
            }
            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await permissionRepository.DeleteAsync(item);
            }
            foreach (var (before, after) in comparisonCollection.GetCollectionByModify())
            {
                before.AreaName = after.AreaName;
                await permissionRepository.UpdateAsync(before);
            }
            await permissionRepository.CommmitAsync();
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>        
        public async Task<IEnumerable<PermissionDto>> Permissions()
        {
            var currUser = currentUserProvider.GetCurrUser();

            var user = await userRepository.GetAsync(currUser.Id);

            if (user.IsAdmin)
                return await permissionRepository.MapTo<Permission, PermissionDto>().ToListAsync();
            else
                return await GetPermissions(user.Id);


        }

        /// <summary>
        /// 获取用户权限
        /// </summary> 
        private async Task<IEnumerable<PermissionDto>> GetPermissions(string userId)
        {
            var iq = from a in roleMemberRepository.Queryable.Where(x => x.User_Id == userId)
                     join b in rolePermissionRepository.Queryable on a.Role_Id equals b.Role_Id
                     join c in permissionRepository.Queryable on b.Permission_Id equals c.Id
                     where c.Parent_Id != null
                     select c;


            var iq2 = from a in iq
                      join b in permissionRepository on a.Parent_Id equals b.Id
                      select b;

            return (await iq.ToListAsync())
                        .Concat(await iq2.ToListAsync())
                        .Distinct(this)
                        .Select(r => r.MapTo<Permission, PermissionDto>());
        }

        /// <summary>
        /// 验证权限
        /// </summary> 
        public async Task<bool> ExistPermission(string moduleName, params string[] methodNames)
        {
            var currUser = currentUserProvider.GetCurrUser();
            //var currUser = await userRepository.GetAsync(userId);
            if (currUser.IsAdmin)
                return true;
            /*一级权限*/
            var permissionId = await permissionRepository
                .Where(r => r.EnCode == moduleName && r.Parent_Id == null)
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            /*二级权限*/
            var permissionIds = await permissionRepository
                .Where(r => r.Parent_Id == permissionId && methodNames.Contains(r.EnCode))
                .Select(r => r.Id)
                .ToListAsync();

            /*是否在所属角色权限中*/
            var query = from a in roleMemberRepository
                        join b in rolePermissionRepository on a.Role_Id equals b.Role_Id
                        where permissionIds.Contains(b.Permission_Id) && a.User_Id == currUser.Id
                        select 1;

            return await query.AnyAsync();
        }


        public bool Equals(Permission x, Permission y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Permission obj)
        {
            return obj.GetHashCode();
        }
    }
}
