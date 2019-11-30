using CSRedis;
using FastFrame.Dto.Module;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Module;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using FastFrame.Service.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Service.Services.Module
{
    public partial class StructureService
    {
        private readonly IRepository<Structure> structures;
        private readonly IRepository<StrucField> strucFields;
        private readonly IRepository<StrucRule> strucRules;
        private readonly IRepository<EntityKeyValue> entityKeyValues;
        private readonly RedisClient redisClient;

        public StructureService(
            IRepository<User> userRepository,
            IScopeServiceLoader loader,
            IRepository<Structure> structures,
            IRepository<StrucField> strucFields,
            IRepository<StrucRule> strucRules,
            IRepository<EntityKeyValue> entityKeyValues,
            RedisClient redisClient) : this(userRepository, structures, loader)
        {
            this.structures = structures;
            this.strucFields = strucFields;
            this.strucRules = strucRules;
            this.entityKeyValues = entityKeyValues;
            this.redisClient = redisClient;
        }

        public async Task<StructureOutput> StructureAsync(string name)
        {
            var keys = new List<string>();
            var structure = await structures.FirstOrDefaultAsync(r => r.Name == name);

            /*取出RelateFields*/
            keys.Add(structure.Id);

            var fieldList = await strucFields.Where(r => r.Struct_Id == structure.Id).ToListAsync();
            var fieldIds = fieldList.Select(r => r.Id).ToList();
            var relateIds = fieldList.Select(r => r.Relate_Id).ToList();

            var relateNames = await structures.Where(r => relateIds.Contains(r.Id)).ToListAsync();

            /*取出EnumValues*/
            keys.AddRange(fieldIds);

            var ruleList = await strucRules.Where(r => fieldIds.Contains(r.Field_Id)).ToListAsync();
            var ruleIds = ruleList.Select(r => r.Id).ToList();
            /*RulePars*/
            keys.AddRange(ruleIds);

            var kvs = await entityKeyValues.Where(r => keys.Contains(r.Source_Id)).ToListAsync();


            var output = new StructureOutput
            {
                Id = structure.Id,
                Description = structure.Description,
                HasManage = structure.HasManage,
                Name = structure.Name,
                RelateFields = kvs.Where(r => r.Source_Id == structure.Id).Select(r => r.Value),
                TreeKeyName = fieldList.FirstOrDefault(r => r.Id == structure.TreeKey_Id)?.Name,
                FieldInfoStruts = fieldList.Select(r => new StrucFieldOutput
                {
                    Id = r.Id,
                    Name = r.Name,
                    DefaultValue = r.DefaultValue,
                    Description = r.Description,
                    EnumValues = entityKeyValues.Where(x => x.Source_Id == r.Id).Select(x => new KeyValuePair<string, string>(x.Key, x.Value)),
                    Hide = r.Hide,
                    IsRequired = r.IsRequired,
                    IsRichText = r.IsRichText,
                    IsTextArea = r.IsTextArea,
                    Readonly = r.Readonly,
                    RelateName = relateNames.FirstOrDefault(x => x.Id == r.Relate_Id)?.Name,
                    Struct_Id = r.Struct_Id,
                    TypeName = r.TypeName,
                    Rules = ruleList.Where(x => r.Id == x.Field_Id).Select(x => new RuleOutPut
                    {
                        Field_Id = x.Field_Id,
                        Id = x.Id,
                        RulePars = kvs.Where(p => p.Source_Id == x.Id).Select(p => p.Value)
                    })
                })
            };

            return output;
        }

        public async Task InitAsync(IEnumerable<StructureOutput> structs)
        {
            if (structs == null)
            {
                throw new System.ArgumentNullException(nameof(structs));
            }
            var befores = await structures.ToListAsync();
            foreach (var item in befores)
            {
                await EventBus?.TriggerAsync(new DoMainDeleteing<StructureOutput>(item.Id));
                await structures.DeleteAsync(item);
            }

            foreach (var item in structs)
            {
                var entity = await structures.AddAsync(new Structure {
                    Description=item.Description,
                    HasManage=item.HasManage,
                    IsDeleted=false,
                    Name=item.Name, 
                });
            }
        }


    }

    public partial class StrucFieldService :
        IEventHandle<DoMainDeleteing<StructureOutput>>
    {
        public async Task HandleEventAsync(DoMainDeleteing<StructureOutput> @event)
        {
            var befores = await strucFieldRepository.Where(r => r.Struct_Id == @event.Id).ToListAsync();
            foreach (var item in befores)
            {
                await EventBus?.TriggerAsync(new DoMainDeleteing<StrucFieldOutput>(item.Id));
                await strucFieldRepository.DeleteAsync(item);
            }

        }
    }

    public partial class StrucRuleService :
        IEventHandle<DoMainDeleteing<StrucFieldOutput>>
    {
        public async Task HandleEventAsync(DoMainDeleteing<StrucFieldOutput> @event)
        {
            var befores = await strucRuleRepository.Where(r => r.Field_Id == @event.Id).ToListAsync();
            foreach (var item in befores)
            {
                await EventBus?.TriggerAsync(new DoMainDeleteing<RuleOutPut>(item.Id));
                await strucRuleRepository.DeleteAsync(item);
            }
        }
    }

    public partial class EntityKeyValueService :
        IEventHandle<DoMainDeleteing<StructureOutput>>,
        IEventHandle<DoMainDeleteing<StrucFieldOutput>>,
        IEventHandle<DoMainDeleteing<RuleOutPut>>
    {
        private async Task DeleteFromSourceId(string id)
        {
            var befores = await entityKeyValueRepository.Where(r => r.Source_Id == id).ToListAsync();
            foreach (var item in befores)
                await entityKeyValueRepository.DeleteAsync(item);

        }
        public Task HandleEventAsync(DoMainDeleteing<StructureOutput> @event)
        {
            return DeleteFromSourceId(@event.Id);
        }

        public Task HandleEventAsync(DoMainDeleteing<StrucFieldOutput> @event)
        {
            return DeleteFromSourceId(@event.Id);
        }

        public Task HandleEventAsync(DoMainDeleteing<RuleOutPut> @event)
        {
            return DeleteFromSourceId(@event.Id);
        }
    }
}
