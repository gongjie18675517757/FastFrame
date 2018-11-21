using FastFrame.Dto.Basis;
using FastFrame.Infrastructure.Attrs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using FastFrame.Infrastructure;
using System.Linq;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service.Services.Basis;

namespace FastFrame.Application.Controllers.Basis
{
    public partial class PermissionController
    {
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
        public async Task<IEnumerable<PermissionDto>> Permissions()
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

                var areaName = type.Namespace.Replace("FastFrame.Application.Controllers.", "");
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
                            Parent = dto
                        });
                    }
                }

                yield return dto;
            }
        }
    }
}
