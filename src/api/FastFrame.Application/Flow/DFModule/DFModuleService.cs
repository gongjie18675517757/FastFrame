using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Application.Flow
{
    public partial class DFModuleService
    {
        private async Task HandleItems(string id, IEnumerable<DFModuleGroupModel> groups)
        { 
            await Loader
                .GetService<HandleOne2ManyService<DFModuleGroup, DFModuleGroup>>()
                .UpdateManyAsync(
                    v => v.DFModule_Id == id,
                    groups.Select((v, i) =>
                    {
                        v.Order = i;
                        return v;
                    }),
                    (a, b) => a.Id == b.Id,
                    v =>
                    {
                        v.DFModule_Id = id;
                        return v;
                    },
                    (before, after) =>
                    {
                        after.MapSet(before);
                    }
                );

            await Loader
               .GetService<HandleOne2ManyService<DFModuleField, DFModuleField>>()
               .UpdateManyAsync(
                    v => v.DFModule_Id == id,
                    groups?.SelectMany(v => v.Fields.Select((x, i) =>
                    {
                        x.Order = i;
                        x.DFModuleGroup_Id = v.Id;
                        x.DFModule_Id = id;
                        return x;
                    })),
                    (a, b) => a.Id == b.Id,
                    v => v,
                    (before, after) =>
                    {
                        after.MapSet(before);
                    }
                );

            await Loader
               .GetService<HandleOne2ManyService<DFModuleFieldRule, DFModuleFieldRule>>()
               .UpdateManyAsync(
                    v => v.DFModule_Id == id,
                    groups?.SelectMany(v => v.Fields.SelectMany(x => x.Rules.Select(y =>
                    {
                        y.DFModuleField_Id = x.Id;
                        y.DFModule_Id = id;
                        return y;
                    }))),
                    (a, b) => a.Id == b.Id,
                    v => v,
                    (before, after) =>
                    {
                        after.MapSet(before);
                    }
                );
        }

        protected override async Task OnAddOrUpdateing(DFModuleDto input, DFModule entity)
        {
            await base.OnAddOrUpdateing(input, entity);

            await HandleItems(entity.Id, input.Groups);
        }

        protected override async Task OnDeleteing(DFModule entity)
        {
            await base.OnDeleteing(entity);

            await HandleItems(entity.Id, null);
        }

        protected override async Task OnGeting(DFModuleDto output)
        {
            await base.OnGeting(output);

            var id = output.Id;

            var dFModuleGroupModels = await Loader
                .GetService<IRepository<DFModuleGroup>>()
                .Where(v => v.DFModule_Id == id)
                .MapTo<DFModuleGroup, DFModuleGroupModel>()
                .ToListAsync();

            var dFModuleFieldModels = await Loader
                .GetService<IRepository<DFModuleField>>()
                .Where(v => v.DFModule_Id == id)
                .MapTo<DFModuleField, DFModuleFieldModel>()
                .ToListAsync();

            var dFModuleFieldRules = await Loader
                .GetService<IRepository<DFModuleFieldRule>>()
                .Where(v => v.DFModule_Id == id)
                .ToListAsync();

            foreach (var f in dFModuleFieldModels)
                f.Rules = dFModuleFieldRules.Where(v => v.DFModuleField_Id == f.Id).ToList();

            foreach (var g in dFModuleGroupModels)
                g.Fields = dFModuleFieldModels.Where(v => v.DFModuleGroup_Id == g.Id).ToList();

            output.Groups = dFModuleGroupModels;
        }
    } 
}
