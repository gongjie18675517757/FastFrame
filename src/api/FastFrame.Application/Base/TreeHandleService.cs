using FastFrame.Entity;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using FastFrame.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using FastFrame.Infrastructure.Module;
using Dapper;
using FastFrame.Infrastructure.Interface;

namespace FastFrame.Application
{
    /// <summary>
    /// 处理树关系
    /// </summary>
    public class TreeHandleService : ITreeHandleService, IService
    {
        private readonly IServiceProvider loader;
        private const int number_length = 3;

        public TreeHandleService(IServiceProvider loader)
        {
            this.loader = loader;
        }

        /// <summary>
        /// 生成树状码
        /// </summary>
        /// <param name="tree_type_name"></param>
        /// <param name="super_id"></param>
        /// <returns></returns>
        [LockMethod]
        public virtual async Task MakeTreeCodeAsync(string tree_type_name, [LockMethodParameter] string super_id)
        {
            if (!TypeManger.TryGetType(tree_type_name, out var type) ||
                !typeof(ITreeEntity).IsAssignableFrom(type))
                return;

            var db = loader.GetService<Database.DataBase>();
            var entityType = db.Model.FindEntityType(type);
            var table_name = entityType.GetTableName();
            var conn = db.Database.GetDbConnection();

            /*取上级树节点信息*/
            var query_sql = $"SELECT id, treecode FROM {table_name} a WHERE a.Id=@super_id";
            var super = await conn.QuerySingleOrDefaultAsync<localTreeModel>(query_sql, new { super_id });
            super ??= new localTreeModel { id = null, treecode = "" };

            /*取下级的树节点*/
            query_sql = $"SELECT id, treecode FROM {table_name} WHERE (super_id = @super_id) or (super_id is null and  @super_id is null)";
            var list = await conn.QueryAsync<localTreeModel>(query_sql, new { super_id });
            var children = list.ToArray();
            if (children.Length == 0)
                return;

            /*更新他下级的树装码*/
            var has_update_list = new List<localTreeModel>();
            for (int i = 0; i < children.Length; i++)
            {
                var tree_item = children[i];
                var tree_code = $"{super.treecode}{(i + 1).ToString().PadLeft(number_length, '0')}";

                if (tree_item.treecode == tree_code)
                    continue;

                tree_item.treecode = tree_code;
                has_update_list.Add(tree_item);
            }

            if (has_update_list.Any())
            {
                /*执行更新*/
                await conn.ExecuteAsync($"update {table_name} set treecode=@treecode where id=@id", has_update_list);

                /*更新他的下级*/
                foreach (var item in children)
                    loader.GetService<IBackgroundJob>().SetTimeout<ITreeHandleService>(v => v.MakeTreeCodeAsync(tree_type_name, item.id), null);
            }
        }

        private class localTreeModel
        {
            public string id { get; set; }

            public string treecode { get; set; }


        }



        /// <summary>
        /// 检测循环引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeEntity"></param>
        /// <param name="value_func"></param>
        /// <returns></returns>
        /// <exception cref="MsgException"></exception>
        public async Task VerifyLoopRefAsync<T>(T treeEntity, Expression<Func<T, IViewModel>> value_func) where T : class, ITreeEntity
        {
            var values = loader.GetService<IRepository<T>>();
            var super_id = treeEntity.Super_Id;

            var list = new List<IViewModel>();
            list.AddRange(new[] { treeEntity }.AsQueryable().Select(value_func));

            while (super_id != null)
            {
                var super = await values.Where(v => v.Id == super_id).Select(v => new { v.Super_Id }).FirstOrDefaultAsync();
                if (super != null)
                    list.Add(await values.Where(v => v.Id == super_id).Select(value_func).FirstOrDefaultAsync());

                if (super_id == treeEntity.Id)
                    throw new MsgException($"检测到循环引用,引用路径:{string.Join(">>", list.Select(v => v.Value))}");

                super_id = super?.Super_Id;
            }
        }

        /// <summary>
        /// 检测循环引用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeEntity"></param>
        /// <returns></returns>
        public Task VerifyLoopRefByViewModelableAsync<T>(T treeEntity) where T : class, ITreeEntity
        {
            if (treeEntity is IViewModelable<T> vv)
                return VerifyLoopRefAsync(treeEntity, vv.GetBuildExpression());

            return VerifyLoopRefAsync(treeEntity, v => new DefaultViewModel { Id = v.Id, Value = v.Id });
        }
    }
}
