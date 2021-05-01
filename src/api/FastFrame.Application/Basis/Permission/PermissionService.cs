using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Permission;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class PermissionService : IPermissionChecker, IService
    {
        private readonly IAppSessionProvider appSessionProvider;
        private readonly IPermissionDefinitionContext permissionDefinitionContext;
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<RoleMember> roleMemberRepository;
        private readonly IRepository<RolePermission> rolePermissionRepository;

        public PermissionService(
                IAppSessionProvider appSessionProvider,
                IPermissionDefinitionContext permissionDefinitionContext,
                IRepository<Role> roleRepository,
                IRepository<RoleMember> roleMemberRepository,
                IRepository<RolePermission> rolePermissionRepository)
        {
            this.appSessionProvider = appSessionProvider;
            this.permissionDefinitionContext = permissionDefinitionContext;
            this.roleRepository = roleRepository;
            this.roleMemberRepository = roleMemberRepository;
            this.rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<bool> CheckIsGrantedAsync(params string[] permissions)
        {
            var permissionDefinitions = permissionDefinitionContext.PermissionDefinitions();

            /*先判断权限是否有定义*/
            if (!permissionDefinitions.Any(v => v.Child.Any(r => permissions.Contains(r.Name))))
                return false;

            var currUser = appSessionProvider.CurrUser;

            if (currUser.IsAdmin)
                return true;

            var existsQuery = from a in roleMemberRepository
                              join b in rolePermissionRepository on a.Role_Id equals b.Role_Id
                              join c in roleRepository on a.Role_Id equals c.Id
                              where (a.User_Id == currUser.Id || c.IsDefault) &&
                                    permissions.Contains(b.PermissionKey)
                              select 1;

            return await existsQuery.AnyAsync();
        }

        public async Task<IEnumerable<PermissionDefinition>> GetGrantedPermissions()
        {
            var permissionDefinitions = permissionDefinitionContext.PermissionDefinitions();

            var currUser = appSessionProvider.CurrUser;

            if (currUser.IsAdmin)
                return permissionDefinitions;

            var existsQuery = from a in roleMemberRepository
                              join b in rolePermissionRepository on a.Role_Id equals b.Role_Id
                              join c in roleRepository on a.Role_Id equals c.Id
                              where (a.User_Id == currUser.Id || c.IsDefault)
                              select b.PermissionKey;

            var permissionArr = await existsQuery.Distinct().ToArrayAsync();

            return permissionDefinitions
                .Where(v => v.Child.Any(r => permissionArr.Contains(r.Name)))
                .Select(v => new PermissionDefinition(
                                    v.Name,
                                    v.Text,
                                    v.Child.Where(r =>
                                            permissionArr.Contains(r.Name))));
        }
    }
}
