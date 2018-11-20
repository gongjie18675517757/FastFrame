using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class PermissionService
    {
        /// <summary>
        /// 初始化权限
        /// </summary>       
        [Permission(nameof(InitPermission), "切换身份")]
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
    }
}
