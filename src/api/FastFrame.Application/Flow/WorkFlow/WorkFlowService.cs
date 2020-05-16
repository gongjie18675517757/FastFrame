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
            dto.Lines = await EventBus.RequestAsync<IEnumerable<FlowLineDto>, WorkFlowDto>(dto);
            dto.Nodes = await EventBus.RequestAsync<IEnumerable<FlowNodeDto>, WorkFlowDto>(dto);
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

        private void VerifyFlowNodes(WorkFlowDto workFlow)
        {
            if (workFlow is null)
                throw new System.ArgumentNullException(nameof(workFlow));

            if (workFlow.Nodes?.Any() == false)
                throw new MsgException("无有效节点！");

            if (workFlow.Nodes.Count(v => v.Key == 0) != 1)
                throw new MsgException("有且只允许有一个起点！");

            if (workFlow.Nodes.Count(v => v.Key == -1) != 1)
                throw new MsgException("有且只允许有一个终点！");

            if (workFlow.Lines?.Any() == false)
                throw new MsgException("节点之间无连线！");

            if (workFlow.Lines.Any(v => v.From == v.To))
                throw new MsgException("连线不正确，不能上下级相同！");

            if (workFlow.Lines.Any(v => v.From == 0 && -1 == v.To))
                throw new MsgException("连线不正确，起点不可直连终点！");

            /*判断节点有没有包含在连线网中*/
            foreach (var node in workFlow.Nodes)
            {
                if (!workFlow.Lines.Any(r => r.From == node.Key || r.To == node.Key))
                    throw new MsgException($"连线不正确，节点：{node.Name}没有连线！");
            }

            /*验证节点连线的正确性*/
            VerifyLines(workFlow.Nodes, workFlow.Lines, 0);
        }

        private void VerifyLines(IEnumerable<FlowNodeDto> flowNodes, IEnumerable<FlowLineDto> flowLines, int from)
        {
            /*上一个节点名称*/
            var fromNode = flowNodes.Where(v => v.Key == from).FirstOrDefault();
            if (fromNode == null)
                throw new MsgException($"连线没有匹配到节点!");

            var lines = flowLines.Where(v => v.From == from);
            if (!lines.Any())
                throw new MsgException($"节点名称:{fromNode.Name}没有下级节点!");

            foreach (var line in lines)
            {
                /*-1表示终点*/
                if (line.To == -1)
                    continue;

                VerifyLines(flowNodes, flowLines, line.To);
            }
        }
    }
}
