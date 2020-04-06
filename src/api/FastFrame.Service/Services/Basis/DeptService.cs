using FastFrame.Dto.Basis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Basis
{
    public partial class DeptService
    {
        protected override async Task OnGeting(DeptDto dto)
        {
            await base.OnGeting(dto);
            var (members, manages) = await EventBus.TriggerRequestAsync<(UserViewModel[] members, string[] manages), string>(dto.Id);
            dto.Members = members;
            dto.Managers = manages;
        }

        protected override async Task OnGetListing(IEnumerable<DeptDto> dtos)
        {
            await base.OnGetListing(dtos);
            var keys = dtos.Select(v => v.Id).ToArray();
            var keyValuePairs = await EventBus.TriggerRequestAsync<IEnumerable<KeyValuePair<string, (UserViewModel[], string[])>>, string[]>(keys);
            foreach (var item in dtos)
            {
                item.Managers = keyValuePairs.Where(v => v.Key == item.Id).SelectMany(v => v.Value.Item2);
                item.Members = keyValuePairs
                    .Where(v => v.Key == item.Id)
                    .SelectMany(v => v.Value.Item1)
                    .Where(v => item.Managers.Contains(v.Id));
            }
        }
    }
}
