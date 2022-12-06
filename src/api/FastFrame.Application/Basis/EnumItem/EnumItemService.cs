using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public override IAsyncEnumerable<ITreeModel> TreeModelListAsync(string super_id)
        {


            return base.TreeModelListAsync(super_id);
        }
    }
}
