using FastFrame.Application.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Basis
{
    [Permission("Permission", "权限管理")]
    public partial class PermissionController : BaseController<Permission, PermissionDto>
    {
        private readonly PermissionService service;

        public PermissionController(PermissionService service) : base(service)
        {
            this.service = service;
        }



        /// <summary>
        /// 初始化权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission(nameof(InitPermission), "初始化权限")]
        public async Task InitPermission()
        {
            var permissions = GetPermissions().ToList();
            await service.InitPermission(permissions);
        }

        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PermissionModel>> Permissions()
        {
            return await service.Permissions();
        }

        private IEnumerable<PermissionDto> GetPermissions()
        {
            foreach (var type in this.GetType().Assembly.GetTypes())
            {
                if (!typeof(BaseController).IsAssignableFrom(type))
                    continue;
                var permissionAttribute = type.GetCustomAttribute<PermissionAttribute>();
                if (permissionAttribute == null)
                    continue;
                if (type.IsAbstract || !type.IsClass)
                    continue;

                var areaName = type.Namespace.Replace("FastFrame.WebHost.Controllers.", "");
                var permissions = new List<PermissionDto>();

                var dto = new PermissionDto()
                {
                    AreaName = areaName,
                    EnCode = permissionAttribute.EnCode,
                    Name = permissionAttribute.Name,
                    Permissions = permissions
                };

                foreach (var prop in type.GetMethods())
                {
                    var attrs = prop.GetCustomAttributes<PermissionAttribute>();
                    if (!attrs.Any())
                        continue;
                    foreach (var attr in attrs)
                    {
                        if (!attr.IsPrimary)
                            continue;
                        permissions.Add(new PermissionDto()
                        {
                            AreaName = areaName,
                            EnCode = attr.EnCode,
                            Name = attr.Name,
                            Super = dto.MapTo<PermissionDto, PermissionViewModel>()
                        });
                    }
                }

                yield return dto;
            }
        }
    }
}
