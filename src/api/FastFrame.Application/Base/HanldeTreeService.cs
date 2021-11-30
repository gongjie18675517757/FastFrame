using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Entity;
using FastFrame.Entity.Basis;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FastFrame.Application.Events;

namespace FastFrame.Application
{
    /// <summary>
    /// 处理树节点
    /// </summary>
    public class HanldeTreeService :
        IService,
        IIntervalWorkHost,
        IApplicationInitialLifetime
    {
        private readonly IServiceProvider loader;
        private readonly IEnumerable<Type> types;

        public HanldeTreeService(IServiceProvider loader)
        {
            this.loader = loader;

            var baseType = typeof(ITreeEntity);
            types = baseType.Assembly.GetTypes().Where(v => v.IsClass && !v.IsAbstract && baseType.IsAssignableFrom(v));
        }

        /// <summary>
        /// 项目启动时执行一次检查
        /// </summary>
        /// <returns></returns>
        public async Task InitialAsync()
        {
            await Task.CompletedTask;
            loader.GetService<IBackgroundJob>().SetTimeout<HanldeTreeService>(v => v.ExistsNotInitAsync(), null);
            loader.GetService<IBackgroundJob>().SetTimeout<HanldeTreeService>(v => v.ExistsNotInitAsync(), null);
            loader.GetService<IBackgroundJob>().SetTimeout<HanldeTreeService>(v => v.ExistsNotInitAsync(), null);
        }

        /// <summary>
        /// 检查是否未初始化
        /// 如果没有执行过树结构计算，则直接计算一次
        /// </summary>
        /// <returns></returns>
        [LockMethod()]
        public virtual async Task ExistsNotInitAsync()
        {
            var treeChildren = loader.GetService<IRepository<TreeChild>>();
            if (!await treeChildren.AnyAsync())
                await HandleCompareChildrenAsync();
        }

        /// <summary>
        /// 每天23:59执行
        /// 比较并更新所有树状节点的递归下级
        /// </summary>
        /// <returns></returns>
        [IntervalWork("59 23 * * *")]
        [LockMethod()]
        public virtual async Task HandleCompareChildrenAsync()
        {
            var method = this.GetType().GetMethod(nameof(HanldTreeLevelBySuperIdAsync));

            foreach (var type in types)
                await (Task)method.MakeGenericMethod(type).Invoke(this, new object[] { new string[0] });
        }

        /// <summary>
        /// 向下计算树层级关联
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="treeKeys"></param>
        /// <returns></returns>
        public async Task HanldTreeLevelBySuperIdAsync<TEntity>(string[] treeKeys) where TEntity : class, IEntity, ITreeEntity
        {
            if (treeKeys.Length == 0)
                treeKeys = new string[] { null };

            foreach (var treeKey in treeKeys)
                await TryMakeChildRelate<TEntity>(treeKey, true);
        }

        /// <summary>
        /// 向上计算层次关联
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="treeKeys"></param>
        /// <returns></returns>
        public async Task HanldTreeLevelByChildIdAsync<TEntity>(string[] treeKeys) where TEntity : class, IEntity, ITreeEntity
        {
            foreach (var treeKey in treeKeys)
            {
                var superIds = await GetLoopSuperId<TEntity>(treeKey);
                foreach (var superId in superIds)
                    await TryMakeChildRelate<TEntity>(superId, false);
            }
        }

        /// <summary>
        /// 向上计算层次关联
        /// </summary> 
        public async Task HanldTreeLevelByChildIdAsync(string typeName, string[] treeKeys)
        {
            if (!types.Any(v => v.Name == typeName))
                throw new MsgException("指定类型不是有效的树结构[ITreeEntity]");

            var type = types.FirstOrDefault(v => v.Name == typeName);
            var method = this.GetType().GetMethods().FirstOrDefault(v => v.Name == nameof(HanldTreeLevelByChildIdAsync) && v.IsGenericMethod);
            await (Task)method.MakeGenericMethod(type).Invoke(this, new object[] { treeKeys });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="treeName"></param>
        /// <param name="id"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        private async Task TrySaveTreeLevelAsync(string treeName, string id, IEnumerable<string> children)
        {
            if (id.IsNullOrWhiteSpace())
                return;
            var logger = loader.GetService<ILogger<HanldeTreeService>>();

            await Task.CompletedTask;

            var treeChildren = loader.GetService<IRepository<TreeChild>>();

            await loader
                .GetService<HandleOne2ManyService<string, TreeChild>>()
                .UpdateManyAsync(
                    v => v.Super_Id == id,
                    children,
                    (a, b) => a.Child_Id == b,
                    v => new TreeChild
                    {
                        Child_Id = v,
                        Super_Id = id,
                        TreeName = treeName
                    }
                );

            try
            {
                await treeChildren.CommmitAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "保存树形关系失败！");
                throw;
            }
        }


        /// <summary>
        /// 尝试建立关系(向下)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="hasLoopChild">是否向下递归</param>
        /// <returns></returns>
        private async Task TryMakeChildRelate<TEntity>(string id, bool hasLoopChild) where TEntity : class, IEntity, ITreeEntity
        {
            var entities = loader.GetService<IRepository<TEntity>>();

            if (!id.IsNullOrWhiteSpace())
            {
                /*递归全部子孙级节点*/
                var loopChildrenIds = new List<string>();
                await GetLoopChildTreeId<TEntity>(id, id, loopChildrenIds);
                await TrySaveTreeLevelAsync(typeof(TEntity).Name, id, loopChildrenIds);
            }

            /*为当前节点的子级建立层级关系*/
            if (hasLoopChild)
            {
                var childrenList = await entities
                    .Where(v => v.Super_Id == id)
                    .Select(v => v.Id)
                    .ToArrayAsync();

                foreach (var childId in childrenList)
                    await TryMakeChildRelate<TEntity>(childId, hasLoopChild);
            }
        }

        /// <summary>
        /// 递归全部下级
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="rootId"></param>
        /// <param name="id"></param>
        /// <param name="resultList"></param>
        /// <returns></returns>
        private async Task GetLoopChildTreeId<TEntity>(string rootId, string id, List<string> resultList) where TEntity : class, IEntity, ITreeEntity
        {
            var entities = loader.GetService<IRepository<TEntity>>();
            const string err_msg = "检查到循环引用";

            var childrenList = await entities
                .Where(v => v.Super_Id == id)
                .Select(v => v.Id)
                .ToArrayAsync();

            foreach (var childrenId in childrenList)
            {
                if (resultList.Contains(childrenId))
                    throw new MsgException($"{err_msg} {rootId}<==>{childrenId}");

                resultList.Add(childrenId);
                await GetLoopChildTreeId<TEntity>(rootId, childrenId, resultList);
            }
        }

        /// <summary>
        /// 递归取全部上级
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<IEnumerable<string>> GetLoopSuperId<TEntity>(string id) where TEntity : class, IEntity, ITreeEntity
        {
            var entities = loader.GetService<IRepository<TEntity>>().Queryable;
            string prevId = id;
            const string err_msg = "检查到循环引用";
            var list = new List<string>() { id };

            while (true)
            {
                var superId = await entities
                    .Where(v => v.Id == prevId)
                    .Select(v => v.Super_Id)
                    .FirstOrDefaultAsync();

                if (superId.IsNullOrWhiteSpace())
                    break;

                if (list.Contains(superId))
                    throw new MsgException($"{err_msg} {superId}<==>{id}");
                list.Add(superId);

                prevId = superId;
            }

            return list;
        }

        /// <summary>
        /// 检查循环引用 
        /// </summary> 
        public async Task ExistsLoopRef(ITreeEntity treeEntity)
        {
            var treeType = treeEntity.GetType();
            var method = this.GetType().GetMethod(nameof(ExistsLoopRefByKey)).MakeGenericMethod(treeType);
            await (Task)method.Invoke(this, new object[] { treeEntity.Super_Id, treeEntity.Id });
        }

        /// <summary>
        /// 检查循环引用
        /// 树节点的上级，不可以是自己，不可以是自己的下级
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="rootKey">要检查的树节点</param>
        /// <param name="key">当前递归到的节点</param>
        /// <returns></returns>
        public async Task ExistsLoopRefByKey<TEntity>(string rootKey, string key) where TEntity : class, IEntity, ITreeEntity
        {
            const string err_msg = "检查到循环引用";
            var entities = loader.GetService<IRepository<TEntity>>().Queryable;

            if (key == rootKey)
                throw new MsgException(err_msg);

            var childrenList = await entities
              .Where(v => v.Super_Id == key)
              .Select(v => v.Id)
              .ToArrayAsync();

            foreach (var child_id in childrenList)
                await ExistsLoopRefByKey<TEntity>(rootKey, child_id);
        }
    }
}
