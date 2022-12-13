using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class EnumItemService : IApplicationInitialLifetime
    {
        public async Task<IEnumerable<EnumItemModel>> GetValues(int? name)
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



        public async Task<IEnumerable<IViewModel>> EnumItemList(int? name, string kw, int page_index = 1, int page_size = 10)
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
                    ChildCount = child_dic.TryGetValueOrDefault((int)enumName),
                    Id = enumName.ToString(),
                    Key = (int)enumName,
                    Super_Id = null,
                    TotalChildCount = total_dic.TryGetValueOrDefault((int)enumName),
                    Value = desProvider.GetEnumSummary(enumName)
                };
            }
        }


        public override IAsyncEnumerable<ITreeModel> TreeListAsync(string super_id, string kw)
        {
            if (super_id == null)
            {
                return TreeModelListAsync();
            }
            else if (int.TryParse(super_id, out var val))
            {
                return BuildTreeModelQueryable(kw).Where(v => v.Super_Id == null && v.Key == val).AsAsyncEnumerable();
            }
            else
            {
                return base.TreeListAsync(super_id, kw);
            }
        }

        protected override IQueryable<EnumItemModel> BuildTreeModelQueryable(string kw)
        {
            return enumItemRepository
                .Where(v => string.IsNullOrWhiteSpace(kw) ||
                            enumItemRepository.Any(x => x.Value.Contains(kw) || x.TreeCode.StartsWith(v.TreeCode)))
                .Select(v => new EnumItemModel
                {
                    Id = v.Id,
                    Super_Id = v.Super_Id,
                    Key = v.Key,
                    ChildCount = enumItemRepository.Count(x => x.Super_Id == v.Id),
                    TotalChildCount = enumItemRepository.Count(x => x.TreeCode.StartsWith(v.TreeCode)),
                    Value = v.Value
                });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns> 
        public async Task InitialAsync()
        {
            var attr_type = typeof(EnumNameForAttribute<>);
            var field_dic = typeof(EnumName)
                .GetFields()
                .Where(v => v.GetCustomAttributesData().Any(x => x.AttributeType.IsGenericType && x.AttributeType.GetGenericTypeDefinition() == attr_type))
                .ToDictionary(v => v.Name, v => v);
            var enumNames = Enum
               .GetValues<EnumName>()
               .Where(v => field_dic.ContainsKey(v.ToString()))
               .ToList();
            var moduleDesProvider = loader.GetService<IModuleDesProvider>();

            foreach (var enumName in enumNames)
            {
                var prev_list = await enumItemRepository.Where(v => v.Key == (int)enumName).Select(v => v.IntKey).ToArrayAsync();

                var field = field_dic[enumName.ToString()];
                var attributeData = field
                    .GetCustomAttributesData()
                    .FirstOrDefault(x => x.AttributeType.IsGenericType && x.AttributeType.GetGenericTypeDefinition() == attr_type);
                var name_to = attributeData.AttributeType.GetGenericArguments().FirstOrDefault();

                var names = Enum.GetNames(name_to);

                foreach (var name in names)
                {
                    var val = (int)Enum.Parse(name_to, name);

                    if (prev_list.Contains(val))
                        continue;

                    await enumItemRepository.AddAsync(new EnumItem
                    {
                        IntKey = val,
                        Key = (int)enumName,
                        SortVal = val,
                        Value = moduleDesProvider.GetEnumSummary(name_to, name),
                        CreateTime = DateTime.Now,
                        Create_User_Id = "00fm5yfgq3q893ylku6uzb57i",
                        Modify_User_Id = "00fm5yfgq3q893ylku6uzb57i",
                        ModifyTime = DateTime.Now,
                        Id = null,
                        Super_Id = null,
                        Tenant_Id = null,
                        TreeCode = null
                    });
                }
            }

            await enumItemRepository.CommmitAsync();

            loader.GetService<IBackgroundJob>().SetTimeout<ITreeHandleService>(v => v.ReCalcTreeCodeAsync(), null);
        }
    }
}
