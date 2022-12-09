using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class EnumItemService
    {
        public async Task<IEnumerable<EnumItemModel>> GetValues(EnumName name)
        {
            return await enumItemRepository
                .Where(v => v.Key == name)
                .OrderBy(v => v.SortVal)
                .ThenBy(v => v.Value)
                .Select(v => new EnumItemModel
                {
                    Id = v.Id,
                    Key = v.Key,
                    Value = v.Value,
                    Super_Id = v.Super_Id
                })

                .ToListAsync();
        }



        public async Task<IEnumerable<IViewModel>> EnumItemList(EnumName? name, string kw, int page_index = 1, int page_size = 10)
        {
            if (name == null)
                return Array.Empty<IViewModel>();

            var query = enumItemRepository.Where(v => v.Key == name).Select(EnumItem.BuildExpression());

            return await query
                .Where(v => kw == null || v.Value.Contains(kw))
                .OrderByDescending(v => v.Id)
                .Skip(page_size * (page_index - 1))
                .Take(page_size)
                .ToListAsync();
        }


        public async IAsyncEnumerable<ITreeModel> TreeModelListAsync()
        {
            var desProvider = loader.GetService<IModuleDesProvider>();
            var total_dic = await enumItemRepository
                  .GroupBy(v => v.Key)
                  .Select(v => new { v.Key, Count = v.Count() })
                  .ToDictionaryAsync(v => v.Key, v => v.Count);

            var child_dic = await enumItemRepository
                  .Where(v => string.IsNullOrWhiteSpace(v.Super_Id))
                  .GroupBy(v => v.Key)
                  .Select(v => new { v.Key, Count = v.Count() })
                  .ToDictionaryAsync(v => v.Key, v => v.Count);

            foreach (var enumName in Enum.GetValues<EnumName>())
            {
                yield return new EnumItemModel
                {
                    ChildCount = child_dic.TryGetValueOrDefault(enumName),
                    Id = enumName.ToString(),
                    Key = enumName,
                    Super_Id = null,
                    TotalChildCount = total_dic.TryGetValueOrDefault(enumName),
                    Value = desProvider.GetEnumSummary(enumName)
                };
            }
        }


        public override IAsyncEnumerable<ITreeModel> TreeModelListAsync(string super_id)
        {
            if (super_id == null)
            {
                return TreeModelListAsync();
            }
            else if (Enum.TryParse<EnumName>(super_id, true, out var val))
            {
                return BuildTreeModelQueryable().Where(v => v.Super_Id == null && v.Key == val).AsAsyncEnumerable();
            }
            else
            {
                return base.TreeModelListAsync(super_id);
            }
        }

        protected override IQueryable<EnumItemModel> BuildTreeModelQueryable()
        {
            return enumItemRepository.Select(v => new EnumItemModel
            {
                Id = v.Id,
                Super_Id = v.Super_Id,
                Key = v.Key,
                ChildCount = enumItemRepository.Count(x => x.Super_Id == v.Id),
                TotalChildCount = enumItemRepository.Count(x => x.TreeCode.StartsWith(v.TreeCode)),
                Value = v.Value
            });
        }
    }
}
