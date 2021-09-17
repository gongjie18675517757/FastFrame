using FastFrame.Entity.Flow;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Application.Flow
{
    public partial class WorkFlowService : IRequestHandle<WorkFlowDto, string>
    {
        public async Task<int> GetLastVersion(string moduleName)
        {
            return await workFlowRepository.Where(v => v.BeModule == moduleName).Select(v => v.Version).OrderByDescending(v => v).FirstOrDefaultAsync() + 1;
        }

        public Task<WorkFlowDto> HandleRequestAsync(string request)
        {
            return GetAsync(request);
        }

        protected override async Task OnGeting(WorkFlowDto dto)
        {
            await base.OnGeting(dto);
        }

        protected override async Task OnDeleteing(WorkFlow entity)
        {
            /*判断流程有没有被使用，有则不允许删除*/
            if (await Loader.GetService<IRepository<FlowInstance>>().AnyAsync(v => v.WorkFlow_Id == entity.Id))
                throw new MsgException("流程使用中，不允许删除,但是你可以禁用该流程！");

            await base.OnDeleteing(entity);
        }

        protected override Task OnAdding(WorkFlowDto input, WorkFlow entity)
        {
            VerifyFlowNodes(input);
            return base.OnAdding(input, entity);
        }

        protected override Task OnUpdateing(WorkFlowDto input, WorkFlow entity)
        {
            VerifyFlowNodes(input);
            return base.OnUpdateing(input, entity);
        }

        /// <summary>
        /// 验证流程节点是否配置正确
        /// </summary>
        /// <param name="workFlow"></param>
        private void VerifyFlowNodes(WorkFlowDto workFlow)
        {
            if (workFlow is null)
                throw new System.ArgumentNullException(nameof(workFlow));

            if (workFlow.Nodes == null || !workFlow.Nodes.Any())
                throw new MsgException("无有效节点");

            if (!workFlow.Nodes.Any(v => v.NodeEnum == FlowNodeEnum.start))
                throw new MsgException("无起点");

            if (!workFlow.Nodes.Any(v => v.NodeEnum == FlowNodeEnum.end))
                throw new MsgException("无终点");

            var nodes = workFlow.Nodes.ToArray();

            for (int i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];

                switch (node.NodeEnum)
                {
                    case FlowNodeEnum.start:
                        if (i != 0)
                            throw new MsgException("起点位置不正确!");
                        break;
                    case FlowNodeEnum.branch:
                    case FlowNodeEnum.check:
                    case FlowNodeEnum.cc:
                    case FlowNodeEnum.cond:
                        VerifyFlowNode(node);
                        break;
                    case FlowNodeEnum.end:
                        if (i != nodes.Length - 1)
                            throw new MsgException("终点位置不正确!");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 验证节点配置是否正确
        /// </summary>
        /// <param name="node"></param>
        private void VerifyFlowNode(FlowNodeModel node)
        {
            switch (node.NodeEnum)
            {
                case FlowNodeEnum.start:
                    throw new MsgException("起点位置不正确!"); 
                case FlowNodeEnum.branch:

                    break;
                case FlowNodeEnum.check:

                    break;
                case FlowNodeEnum.cc:

                    break;
                case FlowNodeEnum.cond:

                    break;
                case FlowNodeEnum.end:
                    throw new MsgException("终点位置不正确!");
                default:
                    break;
            }
        }
    }
}
