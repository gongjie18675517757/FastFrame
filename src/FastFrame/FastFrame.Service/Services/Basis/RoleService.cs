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
        private readonly PermissionRepository permissionRepository;

        public RoleService(ForeignRepository foreignRepository,
            RoleMemberRepository roleMemberRepository,
            RolePermissionRepository rolePermissionRepository,
            PermissionRepository permissionRepository,
            UserRepository userRepository, RoleRepository roleRepository, IScopeServiceLoader loader)
            : this(foreignRepository, userRepository, roleRepository, loader)
        {
            this.roleMemberRepository = roleMemberRepository;
            this.rolePermissionRepository = rolePermissionRepository;
            this.permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 设置角色成员
        /// </summary>         
        public async Task SetRoleMember(string id, IEnumerable<string> users)
        {
            var before = await roleMemberRepository.Queryable.Where(x => x.Role_Id == id).ToListAsync();
            var comparisonCollection = new ComparisonCollection<RoleMember, string>(before, users, (a, b) => a.Role_Id == b);
            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                await roleMemberRepository.AddAsync(new RoleMember()
                {
                    Role_Id = id,
                    User_Id = item
                });
            }
            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await roleMemberRepository.DeleteAsync(item);
            }

            await roleMemberRepository.CommmitAsync();
        }

        /// <summary>
        /// 获取角色成员
        /// </summary> 
        public async Task<IEnumerable<UserDto>> GetRoleMember(string id)
        {
            var iq = from a in roleMemberRepository.Queryable
                     join b in userRepository.Queryable on a.User_Id equals b.Id
                     where a.Role_Id == id
                     select b;

            return await iq.MapTo<User, UserDto>().ToListAsync();
        }


        /// <summary>
        /// 设置角色权限
        /// </summary>  
        public async Task SetRolePermission(string id, IEnumerable<string> permissions)
        {
            var before = await rolePermissionRepository.Queryable.Where(x => x.Role_Id == id).ToListAsync();
            var comparisonCollection = new ComparisonCollection<RolePermission, string>(before, permissions,
                (a, b) => a.Permission_Id == b);
            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                await rolePermissionRepository.AddAsync(new RolePermission() { Role_Id = id, Permission_Id = item });
            }
            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await rolePermissionRepository.DeleteAsync(item);
            }

            await rolePermissionRepository.CommmitAsync();
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PermissionDto>> GetRolePermission(string id)
        {
            var iq = from a in rolePermissionRepository.Queryable
                     join b in permissionRepository.Queryable on a.Permission_Id equals b.Id
                     where a.Role_Id == id
                     select b;
            return await iq.MapTo<Permission, PermissionDto>().ToListAsync();
        }
    }
}
