using Dapper;
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
    public partial class EnumItemService : IApplicationInitialLifetime, IEnumItemProvider
    {
        protected override async Task OnDeleteing(EnumItem entity)
        {
            if (entity.IsSystemEnum)
                throw new MsgException("系统定义的字典不可删除!");

            await base.OnDeleteing(entity);
        }

        public async Task<IReadOnlyDictionary<int, string>> EnumValues(int enum_key)
        {
            var list = await enumItemRepository
                .Where(v => v.KeyEnum == enum_key && v.IntKey != null)
                .OrderBy(v => v.SortVal)
                .ThenBy(v => v.TextValue)
                .ToDictionaryAsync(v => v.IntKey.Value, v => v.TextValue);

            return list;
        }


        public async Task<IEnumerable<IViewModel>> EnumItemList(int? name, string kw, int page_index = 1, int page_size = 10)
        {
            if (name == null)
                return Array.Empty<IViewModel>();

            var query = enumItemRepository.Where(v => v.KeyEnum == name).Select(EnumItem.BuildExpression());

            return await query
                .Where(v => kw == null || v.Value.Contains(kw))
                .OrderByDescending(v => v.Id)
                .Skip(page_size * (page_index - 1))
                .Take(page_size)
                .ToListAsync();
        }





        public override IAsyncEnumerable<ITreeModel> TreeListAsync(string super_id, string kw)
        {
            if (int.TryParse(super_id, out var int_enumName))
            {
                return BuildTreeModelQueryable(kw)
                    .Where(v => v.Key == int_enumName && enumItemRepository.Any(x => x.Id == v.Super_Id && x.KeyEnum == (int)EnumName.EnumNames))
                    .AsAsyncEnumerable();
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
                            enumItemRepository.Any(x => x.TextValue.Contains(kw) || x.TreeCode.StartsWith(v.TreeCode)))
                .Select(v => new EnumItemModel
                {
                    Id = v.Id,
                    Super_Id = v.Super_Id,
                    Key = v.KeyEnum,
                    ChildCount = enumItemRepository.Count(x => x.Super_Id == v.Id),
                    TotalChildCount = enumItemRepository.Count(x => x.TreeCode.StartsWith(v.TreeCode)),
                    Value = v.TextValue,
                    IntKey = v.IntKey,
                    Children = null,
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

            /*生成系统枚举*/
            foreach (var enumName in enumNames)
            {
                var prev_list = await enumItemRepository.Where(v => v.KeyEnum == (int)enumName).Select(v => v.IntKey).ToArrayAsync();

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
                        IsSystemEnum = true,
                        IntKey = val,
                        KeyEnum = (int)enumName,
                        SortVal = val,
                        TextValue = moduleDesProvider.GetEnumSummary(name_to, name),
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

            /*更新上下级关系*/
            var sql = "update basis_enumitem set super_id=@super_id where id=@id and (super_id<>@super_id or super_id is null)";
            var db = loader
                .GetService<Database.DataBase>()
                .Database
                .GetDbConnection();

            var super_dic = await enumItemRepository
                .Where(v => v.KeyEnum == (int)EnumName.EnumNames && v.IsSystemEnum)
                .Select(v => new { v.IntKey, v.Id })
                .ToDictionaryAsync(v => v.IntKey, v => v.Id);

            var root_id = super_dic[0];
            await db.ExecuteAsync(
                    sql,
                    super_dic.Where(v => v.Key != 0).Select(v => v.Value).Select(v => new { id = v, super_id = root_id }).ToList()
                );

            var child_list = await enumItemRepository
                .Where(v => v.KeyEnum != (int)EnumName.EnumNames && v.IsSystemEnum)
                .Select(v => new { v.KeyEnum, v.Id })
                .ToListAsync();



            var update_list = child_list.Select(v => new { id = v.Id, super_id = super_dic[v.KeyEnum] }).ToList();
            await db
                .ExecuteAsync(
                    sql,
                    update_list
                );

            /*生成树状码*/
            loader.GetService<IBackgroundJob>().SetTimeout<ITreeHandleService>(v => v.ReCalcTreeCodeAsync(), null);
        }
    }
}
