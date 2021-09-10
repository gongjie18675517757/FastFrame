using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Basis
{
    public partial class DeptService
    {
        protected override async Task OnGeting(DeptDto dto)
        {
            await base.OnGeting(dto);
            var (members, manages) = await EventBus.RequestAsync<(UserViewModel[] members, string[] manages), DeptDto>(dto);
            dto.Members = members;
            dto.Managers = manages;
        }

        protected override async Task OnGetListing(IEnumerable<DeptDto> dtos)
        {
            await base.OnGetListing(dtos);

            var keyValuePairs = await EventBus.RequestAsync<IEnumerable<KeyValuePair<string, (UserViewModel[], string[])>>, DeptDto[]>(dtos.ToArray());
            foreach (var item in dtos)
            {
                item.Managers = keyValuePairs.Where(v => v.Key == item.Id).SelectMany(v => v.Value.Item2);
                item.Members = keyValuePairs
                    .Where(v => v.Key == item.Id)
                    .SelectMany(v => v.Value.Item1)
                    .Where(v => item.Managers.Contains(v.Id));
            }
        }

        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId(string id)
        {
            return await Query()
               .Where(v => v.Super_Id == id)
               .OrderBy(v => v.EnCode)
               .ThenBy(v => v.Name)
               .MapTo<DeptDto, TreeModel>()
               .ToListAsync();
        }
    }
}
