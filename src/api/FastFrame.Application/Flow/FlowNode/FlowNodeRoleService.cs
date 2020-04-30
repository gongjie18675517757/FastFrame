using FastFrame.Application.Basis;
using FastFrame.Application.Events;
using FastFrame.Entity.Basis;
using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.Application.Flow
{
    public partial class FlowNodeRoleService : IService,
        IEventHandle<DoMainAdding<FlowNodeDto>>,
        IEventHandle<DoMainUpdateing<FlowNodeDto>>,
        IEventHandle<DoMainDeleteing<FlowNodeDto>>,
        IRequestHandle<IEnumerable<KeyValuePair<string, IEnumerable<RoleViewModel>>>, WorkFlowDto>
    {
        private readonly HandleOne2ManyService<RoleViewModel, FlowNodeRole> manyService;
        private readonly IRepository<FlowNodeRole> flowNodeRoles;
        private readonly IRepository<FlowNode> flowNodes;
        private readonly IRepository<Role> roles;

        public FlowNodeRoleService(HandleOne2ManyService<RoleViewModel, FlowNodeRole> manyService,
                                   IRepository<FlowNodeRole> flowNodeRoles,
                                   IRepository<FlowNode> flowNodes,
                                   IRepository<Role> roles)
        {
            this.manyService = manyService;
            this.flowNodeRoles = flowNodeRoles;
            this.flowNodes = flowNodes;
            this.roles = roles;
        }

        private Task HandleItems(string id, IEnumerable<RoleViewModel> items)
        {
            return manyService.UpdateManyAsync(v => v.FlowNode_Id == id, items, (a, b) => a.Role_Id == b.Id, v => new FlowNodeRole
            {
                Id = null,
                Role_Id = v.Id,
                FlowNode_Id = id
            });
        }

        public Task HandleEventAsync(DoMainAdding<FlowNodeDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Roles);
        }

        public Task HandleEventAsync(DoMainUpdateing<FlowNodeDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Roles);
        }

        public Task HandleEventAsync(DoMainDeleteing<FlowNodeDto> @event)
        {
            return HandleItems(@event.Id, null);
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<RoleViewModel>>>> HandleRequestAsync(WorkFlowDto request)
        {
            var roleViews = roles.MapTo<Role, RoleViewModel>();
            var query = from a in flowNodes
                        join b in flowNodeRoles on a.Id equals b.FlowNode_Id
                        join c in roleViews on b.Role_Id equals c.Id
                        where a.WorkFlow_Id == request.Id
                        select new
                        {
                            b.FlowNode_Id,
                            Role = c
                        };

            var list = await query.ToListAsync();
            return list.GroupBy(v => v.FlowNode_Id).Select(v => new KeyValuePair<string, IEnumerable<RoleViewModel>>(v.Key, v.Select(r => r.Role)));
        }
    }
}
