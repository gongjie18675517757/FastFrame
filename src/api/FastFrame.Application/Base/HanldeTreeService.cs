using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.IntervalWork;
using FastFrame.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Entity;
using FastFrame.Entity.Basis;

namespace FastFrame.Application
{
    /// <summary>
    /// 处理树节点
    /// </summary>
    public class HanldeTreeService : IService, IIntervalWorkHost
    {
        private readonly IServiceProvider loader;

        public HanldeTreeService(IServiceProvider loader)
        {
            this.loader = loader;
        }


        /// <summary>
        /// 每分钟一次
        /// </summary>
        /// <returns></returns>
        [IntervalWork("* * * * *")]
        public async Task HandleAsync()
        {
            await Task.CompletedTask;

            Console.WriteLine(DateTime.Now);

            await HanldTreeAsync<EnumItem>();
        }

        /// <summary>
        /// 记录树层级
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="treeKeys"></param>
        /// <returns></returns>
        public async Task HanldTreeAsync<TEntity>(params string[] treeKeys) where TEntity : class, IEntity, ITreeEntity
        {
            await Task.Yield();
            var entities = loader.GetService<IRepository<TEntity>>();
            var tbName = entities.GetDbTableName();
            var treeSuperName = entities.GetDbColumnName(nameof(ITreeEntity.Super_Id));

            Console.WriteLine(tbName);
            Console.WriteLine(treeSuperName);

            //foreach (var treeKey in treeKeys)
            //{
            //    var before
            //}



            /*在数据库中批量创建/比对修改上级树结点，对所有下级树结点的引用*/
        }
    }
}
