using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastFrame.Service
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntities">实体仓库服务</param>
        public HandleOne2ManyService(IRepository<TTargetEntity> targetEntities)
        {
            this.targetEntities = targetEntities;
        }

        /// <summary>
        /// 添加多个
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="makeEntiyFunc">生成实体</param>
        /// <returns></returns>
        public async Task AddManyAsync(IEnumerable<TTargetDto> list, Func<TTargetDto, TTargetEntity> makeEntiyFunc)
        {
            if (makeEntiyFunc == null)
            {
                throw new ArgumentNullException(nameof(makeEntiyFunc));
            }

            if (list != null)
            {
                foreach (var item in list)
                {
                    var itemEntity = makeEntiyFunc(item);
                    await targetEntities.AddAsync(itemEntity);
                }
            }


        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public async Task DelManyAsync(Expression<Func<TTargetEntity, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var befores = await targetEntities.Where(expression).ToListAsync();
            foreach (var item in befores)
            {
                await targetEntities.DeleteAsync(item);
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
                    Expression<Func<TTargetEntity, bool>> expression,
                    IEnumerable<TTargetDto> list,
                    Func<TTargetEntity, TTargetDto, bool> compareFunc,
                    Func<TTargetDto, TTargetEntity> makeEntiyFunc,
                    Action<TTargetEntity, TTargetDto> updateAction = null)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (compareFunc == null)
            {
                throw new ArgumentNullException(nameof(compareFunc));
            }

            if (makeEntiyFunc == null)
            {
                throw new ArgumentNullException(nameof(makeEntiyFunc));
            }

            var befores = await targetEntities.Where(expression).ToListAsync();
            var comparisonCollection = new ComparisonCollection<TTargetEntity, TTargetDto>(befores, list, compareFunc);

            foreach (var item in comparisonCollection.GetCollectionByAdded())
            {
                var itemEntity = makeEntiyFunc(item);
                await targetEntities.AddAsync(itemEntity);
            }

            foreach (var item in comparisonCollection.GetCollectionByDeleted())
            {
                await targetEntities.DeleteAsync(item);
            }
            foreach (var (before, after) in comparisonCollection.GetCollectionByModify())
            {
                if (updateAction != null)
                {
                    updateAction(before, after);
                    await targetEntities.UpdateAsync(before);
                }
            }
        }
    }
}
