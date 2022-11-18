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

        public async Task<IEnumerable<ITreeModel>> GetChildren(EnumName name)
        {
            return await Query()
                .Where(v => v.Key == name && v.Super_Id == null)
                .OrderBy(v => v.SortVal)
                .ThenBy(v => v.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<ITreeModel>> GetChildren(string id)
        {
            return await Query()
                .Where(v => v.Super_Id == id)
                .OrderBy(v => v.SortVal)
                .ThenBy(v => v.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<EnumName?>> GetHasChildrenNames()
        {
            return await enumItemRepository
                .Select(v => v.Key)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IPageList<EnumItemViewModel>> EnumItemList(EnumName? name, IPagination<EnumItemViewModel> pagination)
        {
            if (name == null)
                return new PageList<EnumItemViewModel>();

            return await enumItemRepository.Where(v => v.Key == name).MapTo<EnumItem, EnumItemViewModel>().PageListAsync(pagination);
        }
    }
}
