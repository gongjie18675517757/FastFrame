using FastFrame.Application.Events;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    /// <summary>
    /// 处理一对多
    /// </summary>
    /// <typeparam name="TTargetDto">DTO类型</typeparam>
    /// <typeparam name="TTargetEntity">实体类型</typeparam>
    public class HandleOne2ManyService<TTargetDto, TTargetEntity>
        where TTargetEntity : class, IEntity
    {
        private readonly IRepository<TTargetEntity> targetEntities;
        private readonly IEventBus eventBus;
        private readonly List<(TTargetDto input, TTargetEntity entity)> addedList = new();
        private readonly List<(TTargetDto after, TTargetEntity before)> updatedList = new();
        private readonly List<TTargetEntity> deletedList = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntities">实体仓库服务</param>
        /// <param name="eventBus"></param>
        public HandleOne2ManyService(IRepository<TTargetEntity> targetEntities, IEventBus eventBus)
        {
            this.targetEntities = targetEntities;
            this.eventBus = eventBus;
        }

        public IReadOnlyList<(TTargetDto input, TTargetEntity entity)> AddedList { get => addedList; }
        public IReadOnlyList<(TTargetDto after, TTargetEntity before)> UpdatedList { get => updatedList; }
        public IReadOnlyList<TTargetEntity> DeletedList { get => deletedList; }


        /// <summary>
        /// 添加多个
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="makeEntiyFunc">生成实体</param>
        /// <returns></returns>
        public async Task AddManyAsync(IEnumerable<TTargetDto> list, Func<TTargetDto, TTargetEntity> makeEntiyFunc!!)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    var itemEntity = makeEntiyFunc(item);
                    await AddItem(itemEntity, item);
                }
            }
        }

        private async Task AddItem(TTargetEntity itemEntity, TTargetDto input)
        {
            itemEntity = await targetEntities.AddAsync(itemEntity);

            addedList.Add((input, itemEntity));

            if (input is IDto<TTargetEntity>)
                await eventBus.TriggerEventAsync(new DoMainAdding<TTargetDto>(input));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public async Task DelManyAsync(Expression<Func<TTargetEntity, bool>> expression!!)
        {
            var befores = await targetEntities.Where(expression).ToListAsync();
            foreach (var item in befores)
            {
                await DeleteItem(item);
            }
        }

        private async Task DeleteItem(TTargetEntity item)
        {
            await targetEntities.DeleteAsync(item);
            deletedList.Add(item);

            var type = typeof(TTargetDto);

            if (type.IsClass && typeof(IDto<>).MakeGenericType(typeof(TTargetEntity)).IsAssignableFrom(type))
            {
                await eventBus.TriggerEventAsync(new DoMainDeleteing<TTargetDto>(item.Id, item));
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="list">列表</param>
        /// <param name="compareFunc">比较表达式</param>
        /// <param name="makeEntiyFunc">构造实体</param>
        /// <param name="updateAction">更新方法</param>
        /// <returns></returns>
        public async Task UpdateManyAsync(
                    Expression<Func<TTargetEntity, bool>> expression!!,
                    IEnumerable<TTargetDto> list,
                    Func<TTargetEntity, TTargetDto, bool> compareFunc!!,
                    Func<TTargetDto, TTargetEntity> makeEntiyFunc!!,
                    Action<TTargetEntity, TTargetDto> updateAction = null)
        {
            var befores = await targetEntities.Where(expression).ToListAsync();
            var comparisonCollection = new ComparisonCollection<TTargetEntity, TTargetDto>(befores, list, compareFunc);

            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                var itemEntity = makeEntiyFunc(item);
                await AddItem(itemEntity, item);
            }

            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await DeleteItem(item);
            }
            foreach (var (before, after) in comparisonCollection.GetCollectionByModify())
            {
                if (updateAction != null)
                {
                    updateAction(before, after);
                    await UpdateItem(before, after);
                }
            }
        }

        private async Task UpdateItem(TTargetEntity before, TTargetDto after)
        {
            await targetEntities.UpdateAsync(before);

            updatedList.Add((after, before));

            if (after is IDto<TTargetEntity>)
                await eventBus.TriggerEventAsync(new DoMainUpdateing<TTargetDto>(after, before));
        }
    }
}
