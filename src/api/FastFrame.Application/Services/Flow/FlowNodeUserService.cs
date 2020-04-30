using FastFrame.Dto.Flow;
using FastFrame.Entity.Flow;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastFrame.Repository;
using System.Linq;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Application.Events;
using Microsoft.EntityFrameworkCore;
using FastFrame.Dto.Basis;
using FastFrame.Entity.Basis;
using FastFrame.Infrastructure;

namespace FastFrame.Application.Services.Flow
{
    public partial class FlowNodeUserService : IService,
        IEventHandle<DoMainAdding<FlowNodeDto>>,
        IEventHandle<DoMainUpdateing<FlowNodeDto>>,
        IEventHandle<DoMainDeleteing<FlowNodeDto>>,
        IRequestHandle<IEnumerable<KeyValuePair<string, IEnumerable<UserViewModel>>>, WorkFlowDto>
    {
        private readonly HandleOne2ManyService<UserViewModel, FlowNodeUser> manyService;
        private readonly IRepository<FlowNode> flowNodes;
        private readonly IRepository<FlowNodeUser> flowNodeUsers;
        private readonly IRepository<User> users;

        public FlowNodeUserService(HandleOne2ManyService<UserViewModel, FlowNodeUser> manyService,
                                   IRepository<FlowNode> flowNodes,
                                   IRepository<FlowNodeUser> flowNodeUsers,
                                   IRepository<User> users)
        {
            this.manyService = manyService;
            this.flowNodes = flowNodes;
            this.flowNodeUsers = flowNodeUsers;
            this.users = users;
        }

        private Task HandleItems(string id, IEnumerable<UserViewModel> items)
        {
            return manyService.UpdateManyAsync(v => v.FlowNode_Id == id, items, (a, b) => a.User_Id == b.Id, v => new FlowNodeUser
            {
                Id = null,
                User_Id = v.Id,
                FlowNode_Id = id
            });
        }

        public Task HandleEventAsync(DoMainAdding<FlowNodeDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Users);
        }

        public Task HandleEventAsync(DoMainUpdateing<FlowNodeDto> @event)
        {
            return HandleItems(@event.Data.Id, @event.Data.Users);
        }

        public Task HandleEventAsync(DoMainDeleteing<FlowNodeDto> @event)
        {
            return HandleItems(@event.Id, null);
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<UserViewModel>>>> HandleRequestAsync(WorkFlowDto request)
        {
            var userViewModels = users.MapTo<User, UserViewModel>();
            var query = from a in flowNodes
                        join b in flowNodeUsers on a.Id equals b.FlowNode_Id
                        join c in userViewModels on b.User_Id equals c.Id
                        where a.WorkFlow_Id == request.Id
                        select new
                        {
                            b.FlowNode_Id,
                            User = c
                        };

            var list = await query.ToListAsync();

            return list.GroupBy(v => v.FlowNode_Id).Select(v => new KeyValuePair<string, IEnumerable<UserViewModel>>(v.Key, v.Select(r => r.User)));
        }
    }
}
