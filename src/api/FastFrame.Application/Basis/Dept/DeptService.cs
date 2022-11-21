using FastFrame.Entity;
using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            var dic = await Loader.GetService<DeptMemberService>().GetUserViewModelsByDeptIds(dto.Id);
            dto.Members = dic.Values.SelectMany(v => v.members);
            dto.Managers = dic.Values.SelectMany(v => v.mangers);
        }

        protected override async Task OnGetListing(IEnumerable<DeptDto> dtos)
        {
            await base.OnGetListing(dtos);

            var dic = await Loader.GetService<DeptMemberService>().GetUserViewModelsByDeptIds(dtos.Select(v => v.Id).ToArray());
            foreach (var item in dtos)
            {
                var (members, mangers) = dic.TryGetValueOrDefault(item.Id);
                item.Members = members ?? Array.Empty<IViewModel>();
                item.Managers = mangers ?? Array.Empty<string>();
            }
        }

        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId(string id)
        {
            return await BuildQuery()
               .Where(v => v.Super_Id == id)
               .OrderBy(v => v.TreeCode)
               .ThenBy(v => v.Name)
               .MapTo<DeptDto, TreeModel>()
               .ToListAsync();
        }
    }
}
