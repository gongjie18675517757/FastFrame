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
    public partial class PermissionService
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
            var iq = GetPermissions(user.Id);

            if (user.IsAdmin)
            {
                iq = permissionRepository.Queryable;
            }
            var list = await iq.MapTo<Permission, PermissionDto>().ToListAsync();
            return list;
        }

        private IQueryable<Permission> GetPermissions(string userId)
        {
            var iq = from a in roleMemberRepository.Queryable.Where(x => x.User_Id == userId)
                     join b in rolePermissionRepository.Queryable on a.Role_Id equals b.Role_Id
                     join c in permissionRepository.Queryable on b.Permission_Id equals c.Id
                     where c.Parent_Id != null
                     select c;

            var iq2 = from a in iq.Select(x => x.Parent_Id).Distinct()
                      join b in permissionRepository.Queryable on a equals b.Id
                      select b;

            iq = iq.Concat(iq2).Distinct();

            return iq;
        }

        public async Task<bool> ExistPermission(string moduleName, params string[] methodNames)
        {
            var currUser = currentUserProvider.GetCurrUser();
            //var currUser = await userRepository.GetAsync(userId);
            if (currUser.IsAdmin)
                return true;

            var permissionId = await permissionRepository.Queryable
                .Join(permissionRepository.Queryable, x => x.Id, x => x.Parent_Id, (a, b) => new { a, b })
                .Where(x => x.a.EnCode == moduleName && methodNames.Contains(x.b.EnCode))
                .Select(x => x.b.Id)
                .FirstOrDefaultAsync();

            var iq = GetPermissions(currUser.Id).Where(x => x.Id == permissionId);
            return await iq.AnyAsync();
        }
    }
}
