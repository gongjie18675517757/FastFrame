using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository.Basis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class RoleService
    {
        private readonly RoleMemberRepository roleMemberRepository;
        private readonly RolePermissionRepository rolePermissionRepository;

        public RoleService(ForeignRepository foreignRepository,
            RoleMemberRepository roleMemberRepository,
            RolePermissionRepository rolePermissionRepository,
            UserRepository userRepository, RoleRepository roleRepository, IScopeServiceLoader loader)
            : this(foreignRepository, userRepository,roleRepository, loader)
        {
            this.roleMemberRepository = roleMemberRepository;
            this.rolePermissionRepository = rolePermissionRepository;
        }

        /// <summary>
        /// 设置角色成员
        /// </summary>         
        public async Task SetRoleMember(string id,IEnumerable<UserDto> users)
        {
            var before =await roleMemberRepository.Queryable.Where(x => x.Role_Id == id).ToListAsync();
            var comparisonCollection = new ComparisonCollection<RoleMember, UserDto>(before, users, (a, b) => a.Role_Id == b.Id);
            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                await roleMemberRepository.AddAsync(new RoleMember()
                {
                    Role_Id = id,
                    User_Id = item.Id
                });
            }
            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await roleMemberRepository.DeleteAsync(item);
            }

            await roleMemberRepository.CommmitAsync();
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>  
        public async Task SetRolePermission(string id, IEnumerable<PermissionDto> permissions)
        {
            var before = await rolePermissionRepository.Queryable.Where(x => x.Role_Id == id).ToListAsync();
            var comparisonCollection = new ComparisonCollection<RolePermission, PermissionDto>(before, permissions, (a, b) => a.Permission_Id == b.Id);
            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                await rolePermissionRepository.AddAsync(new RolePermission() { Role_Id = id, Permission_Id = item.Id });
            }
            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await rolePermissionRepository.DeleteAsync(item);
            }

            await rolePermissionRepository.CommmitAsync();
        }
    }
}
