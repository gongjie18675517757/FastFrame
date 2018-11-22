using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository.Basis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class PermissionService
    {
        private readonly RoleRepository roleRepository;
        private readonly RoleMemberRepository roleMemberRepository;
        private readonly RolePermissionRepository rolePermissionRepository;
        private readonly ICurrentUserProvider currentUserProvider;

        public PermissionService(
            PermissionRepository permissionRepository,
            ForeignRepository foreignRepository,
            UserRepository userRepository,
            RoleRepository roleRepository,
            RoleMemberRepository roleMemberRepository,
            RolePermissionRepository rolePermissionRepository,
            ICurrentUserProvider currentUserProvider,
            IScopeServiceLoader loader)
            : this(permissionRepository, foreignRepository, userRepository, loader)
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
                (a, b) => a.AreaName == b.AreaName && a.EnCode == b.EnCode && a.Parent_Id == b.Parent_Id);
            foreach (var item in comparisonCollection.GetCollectionByAdded().ToList())
            {
                var entity = item.MapTo<PermissionDto, Permission>();
                entity = await permissionRepository.AddAsync(entity);
                beforeEntitys.Add(entity);
                item.Id = entity.Id;
                foreach (var child in item.Permissions)
                    child.Parent_Id = entity.Id;
            }

            foreach (var (before, after) in comparisonCollection.GetCollectionByModify())
            {
                after.Id = before.Id;
                foreach (var child in after.Permissions)
                    child.Parent_Id = before.Id;
            }

            comparisonCollection = new ComparisonCollection<Permission, PermissionDto>(
                    beforeEntitys,
                    permissionDtos.SelectMany(x => x.Permissions).Concat(permissionDtos),
                    (a, b) => a.AreaName == b.AreaName && a.EnCode == b.EnCode && a.Parent_Id == b.Parent_Id);

            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                var entity = item.MapTo<PermissionDto, Permission>();
                entity.Parent_Id = item.Parent.Id;
                entity = await permissionRepository.AddAsync(entity);
            }

            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await permissionRepository.DeleteAsync(item);
            }
            foreach (var (before, after) in comparisonCollection.GetCollectionByModify())
            {
                if (before.Id != after.Id || before.Parent_Id != after.Parent?.Id)
                {
                    var id = before.Id;
                    after.MapSet(before);
                    before.Parent_Id = after.Parent?.Id;
                    before.Id = id;
                    await permissionRepository.UpdateAsync(before);
                }
            }
            await permissionRepository.CommmitAsync();
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>        
        public async Task<IEnumerable<PermissionDto>> Permissions()
        {
            var currUser = currentUserProvider.GetCurrUser();

            var user =await userRepository.GetAsync(currUser.Id);

            var iq = from a in roleMemberRepository.Queryable.Where(x => x.User_Id == user.Id)
                     join b in rolePermissionRepository.Queryable on a.Role_Id equals b.Role_Id
                     join c in permissionRepository.Queryable on b.Permission_Id equals c.Id
                     select c;
            var iq2 = from a in iq.GroupBy(x => x.Parent_Id)
                      join b in permissionRepository.Queryable on a.Key equals b.Id
                      select b;

            iq = iq.Concat(iq2);

            if (currUser.IsAdmin)
            {
                iq = permissionRepository.Queryable;
            }

            var list = await iq.MapTo<Permission, PermissionDto>().ToListAsync();
            return list.GroupBy(x => x.Id).Select(x => x.FirstOrDefault());
        }
    }
}
