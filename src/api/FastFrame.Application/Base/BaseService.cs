using AspectCore.DependencyInjection;
using FastFrame.Application.Events;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using FastFrame.Repository.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using static FastFrame.Application.Account.IdentityManagerService;
using FastFrame.Application.Basis;
using FastFrame.Entity.Basis;
using AspectCore.Extensions.Reflection;
using static Dapper.SqlMapper;

namespace FastFrame.Application
{
    public abstract class BaseService<TDto> : IPageListService<TDto> where TDto : class
    {
        public BaseService(IServiceProvider loader)
        {
            this.loader = loader;
        }

        protected virtual IServiceProvider loader { get; set; }

        protected IEventBus EventBus => loader.GetService<IEventBus>();

        protected IApplicationSession AppSession => loader.GetService<IApplicationSession>();

        /// <summary>
        /// 返回前
        /// </summary> 
        protected virtual async Task OnGeting(TDto output)
        {
            if (output is IHaveMultiFileDto haveMultiFile)
                haveMultiFile.Files = await EventBus.RequestAsync<IEnumerable<ResourceModel>, IHaveMultiFileDto>(haveMultiFile);

            if (output is IHaveCheckModel haveCheckModel)
            {
                haveCheckModel.CheckerIds = await EventBus.RequestAsync<IEnumerable<string>, IHaveCheckModel>(haveCheckModel);
                haveCheckModel.StepList = await EventBus.RequestAsync<IEnumerable<Flow.FlowStepModel>, IHaveCheckModel>(haveCheckModel);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>   
        public virtual async Task<TDto> GetAsync(string id)
        {
            if (id.IsNullOrWhiteSpace())
                throw new NotFoundException();

            var expression = ExpressionClosureFactory.BuildEqualExpression<TDto, string>("Id", id);
            var dto = await BuildQuery().FirstOrDefaultAsync(expression);

            if (dto == null)
                throw new NotFoundException();

            await OnGeting(dto);
            return dto;
        }

        protected virtual IQueryable<TDto> GetListQueryableing(IQueryable<TDto> query, IPagination<TDto> pageInfo) => query;

        /// <summary>
        /// 返回列表数据时
        /// </summary> 
        protected virtual async Task OnGetListing(IEnumerable<TDto> dtos)
        {
            if (typeof(IHaveMultiFileDto).IsAssignableFrom(typeof(TDto)))
            {
                var haveMultiFiles = dtos.Cast<IHaveMultiFileDto>();
                var valuePairs = await EventBus
                    .RequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<ResourceModel>>>, IEnumerable<IHaveMultiFileDto>>(haveMultiFiles);

                foreach (var fileDto in haveMultiFiles)
                {
                    fileDto.Files = valuePairs.Where(v => v.Key == fileDto.Id).SelectMany(v => v.Value);
                }
            }

            /*填充单据的流程信息*/
            if (typeof(IHaveCheckModel).IsAssignableFrom(typeof(TDto)))
            {
                var haveCheckModels = dtos.Cast<IHaveCheckModel>();

                var valuePairs = await EventBus
                  .RequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<string>>>, IEnumerable<IHaveCheckModel>>(haveCheckModels);

                var valuePairs2 = await EventBus
                  .RequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<Flow.FlowStepModel>>>, IEnumerable<IHaveCheckModel>>(haveCheckModels);

                foreach (var haveCheckModel in haveCheckModels)
                {
                    haveCheckModel.CheckerIds = valuePairs.Where(v => v.Key == haveCheckModel.Id).SelectMany(v => v.Value);
                    haveCheckModel.StepList = valuePairs2.Where(v => v.Key == haveCheckModel.Id).SelectMany(v => v.Value);
                }
            }
        }

        /// <summary>
        /// 获取分页列表
        /// </summary> 
        public virtual async Task<IPageList<TDto>> PageListAsync(IPagination<TDto> pageInfo)
        {
            var query = BuildQuery();
            query = GetListQueryableing(query, pageInfo);
            var pageList = await query.PageListAsync(pageInfo);

            await OnGetListing(pageList.Data);
            return pageList;
        }

        /// <summary>
        /// 代码生成器生成的查询表达式
        /// </summary> 
        protected abstract IQueryable<TDto> DefaultQueryable();

        /// <summary>
        /// 构建主查询表达式
        /// </summary>
        /// <returns></returns>
        internal virtual IQueryable<TDto> BuildQuery() => DefaultQueryable();



        /// <summary>
        /// 代码生成器生成的vm查询表达式
        /// </summary>
        /// <returns></returns> 
        protected virtual IQueryable<IViewModel> DefaultViewModelQueryable()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 构建vm查询表达式
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual IQueryable<IViewModel> BuildViewModelList()
        {
            return DefaultViewModelQueryable();
        }

        /// <summary>
        /// vm列表
        /// </summary>
        /// <param name="kw"></param>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public virtual IAsyncEnumerable<IViewModel> ViewModelListAsync(string kw, int page_index = 1, int page_size = 10)
        {
            var query = BuildViewModelList();

            var list = query
               .Where(v => kw == null || v.Value.Contains(kw))
               .OrderByDescending(v => v.Value)
               .Skip(page_size * (page_index - 1))
               .Take(page_size)
               .AsAsyncEnumerable();

            return list;
        }

        /// <summary>
        /// 代码生成器生成的tree_model查询表达式
        /// </summary>
        /// <returns></returns> 
        protected virtual IQueryable<ITreeModel> DefaultTreeModelQueryable()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 构建tree_model查询表达式
        /// </summary>
        /// <returns></returns> 
        protected virtual IQueryable<ITreeModel> BuildTreeModelQueryable()
        {
            return DefaultTreeModelQueryable();
        }

        /// <summary>
        /// 树层次查询
        /// </summary>
        /// <param name="super_id"></param>
        /// <returns></returns>
        public virtual IAsyncEnumerable<ITreeModel> TreeModelListAsync(string super_id)
        {
            //var repository = loader.GetService<IRepository<TTreeEntity>>();
            //var main_query = repository.Where(expression);
            //var value_query = repository.Select(TTreeEntity.BuildExpression());

            //var query = from a in main_query
            //            join b in value_query on a.Id equals b.Id
            //            select new TreeModel
            //            {
            //                Id = a.Id,
            //                Super_Id = a.Super_Id,
            //                Value = b.Value,
            //                ChildCount = repository.Count(v => v.Super_Id == a.Id),
            //                TotalChildCount = repository.Count(v => v.Id != a.Id && v.TreeCode.StartsWith(a.TreeCode)),
            //            };

            return BuildTreeModelQueryable().Where(v => v.Super_Id == super_id).AsAsyncEnumerable();
        }
    }

    /// <summary>
    /// 服务层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public class BaseService<TEntity, TDto> : BaseService<TDto>, ICURDService<TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        protected readonly IRepository<TEntity> repository;

        public BaseService(IServiceProvider loader, IRepository<TEntity> repository) : base(loader)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 新增时/修改时/删除时
        /// </summary>
        /// <param name="input"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual async Task OnChangeing(TDto input, TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnAddOrUpdateing(TDto input, TEntity entity)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 新增时
        /// </summary> 
        protected virtual async Task OnAdding(TDto input, TEntity entity)
        {
            await OnAddOrUpdateing(input, entity);
            await OnChangeing(input, entity);

            if (entity is IHaveNumber haveNumber)
                await loader.GetService<IAutoNumberService>().TryMakeNumberAsync(entity);

            if (input is IHaveMultiFileDto haveMultiFile)
                await EventBus.TriggerEventAsync(new DoMainAdding<IHaveMultiFileDto>(haveMultiFile, entity));

            /*验证循环引用*/
            if (entity is ITreeEntity && entity is IViewModelable<TEntity>)
            {
                var handler = loader.GetService<ITreeHandleService>();
                var method = handler.GetType().GetMethod(nameof(ITreeHandleService.VerifyLoopRefByViewModelableAsync))
                                    .MakeGenericMethod(typeof(TEntity)).GetReflector();
                var task = (Task)method.Invoke(handler, new object[] { entity });
                await task;
            }

            await EventBus?.TriggerEventAsync(new DoMainAdding<TDto>(input));
        }



        /// <summary>
        /// 新增
        /// </summary>  
        public virtual async Task<string> AddAsync(TDto input)
        {
            var entity = input.MapTo<TDto, TEntity>();

            /*写审计字段*/
            if (entity is IHasManage hasManage)
            {
                hasManage.CreateTime = DateTime.Now;
                hasManage.ModifyTime = DateTime.Now;
                var currUser = AppSession?.CurrUser;
                if (currUser != null)
                {
                    hasManage.Create_User_Id = currUser?.Id;
                    hasManage.Modify_User_Id = currUser?.Id;
                }
            }

            //await repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            await repository.AddAsync(entity);

            input.Id = entity.Id;
            await OnAdding(input, entity);
            await repository.CommmitAsync();

            if (entity is ITreeEntity treeEntity)
            {
                var type_name = typeof(TEntity).Name;
                var super_id = treeEntity.Super_Id;
                loader.GetService<IBackgroundJob>().SetTimeout<ITreeHandleService>(v => v.MakeTreeCodeAsync(type_name, super_id), null);
            }

            return entity.Id;
        }

        /// <summary>
        /// 删除前
        /// </summary> 
        protected virtual async Task OnDeleteing(TEntity entity)
        {
            await OnChangeing(null, entity);

            await EventBus?.TriggerEventAsync(new DoMainDeleteing<TDto>(entity.Id, entity));

            if (entity is IHaveMultiFile)
                await EventBus.TriggerEventAsync(new DoMainDeleteing<IHaveMultiFileDto>(entity.Id, entity));
        }


        /// <summary>
        /// 删除
        /// </summary>  
        public virtual async Task DeleteAsync(params string[] ids)
        {
            var entitys = await repository
                        .Where(v => ids.Contains(v.Id))
                        .ToListAsync();

            //await repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            foreach (var entity in entitys)
            {
                await repository.DeleteAsync(entity);
                await OnDeleteing(entity);

                await EventBus.TriggerEventAsync(new DoMainDeleteing<TDto>(entity.Id, entity));

                if (entity is IHaveCheck haveCheck)
                    await EventBus.TriggerEventAsync(new DoMainDeleteing<IHaveCheckModel>(entity.Id, entity));

                if (entity is ITreeEntity treeEntity)
                {
                    /*有下级，则不可删除*/
                    var expression = ExpressionClosureFactory.BuildEqualExpression<TEntity, string>(nameof(ITreeEntity.Super_Id), entity.Id);
                    if (await repository.AnyAsync(expression))
                        throw new MsgException($"{treeEntity}不可删除,因为他还有下级");
                }
            }
            await repository.CommmitAsync();
        }

        /// <summary>
        /// 更新时
        /// </summary> 
        protected virtual async Task OnUpdateing(TDto input, TEntity entity)
        {
            await OnAddOrUpdateing(input, entity);
            await OnChangeing(input, entity);

            await EventBus?.TriggerEventAsync(new DoMainUpdateing<TDto>(input, entity));

            if (input is IHaveMultiFileDto haveMultiFile)
                await EventBus.TriggerEventAsync(new DoMainUpdateing<IHaveMultiFileDto>(haveMultiFile, entity));

            /*验证循环引用*/
            if (entity is ITreeEntity)
            {
                var handler = loader.GetService<ITreeHandleService>();

                var method = handler.GetType().GetMethod(nameof(ITreeHandleService.VerifyLoopRefByViewModelableAsync));
                var methodReflector = method.MakeGenericMethod(typeof(TEntity));

                var task = (Task)methodReflector.Invoke(handler, new object[] { entity });
                await task;
            }
        }

        /// <summary>
        /// 更新前
        /// </summary> 
        protected virtual Task OnBeforeUpdate(TEntity before, TDto after)
            => Task.CompletedTask;

        /// <summary>
        /// 更新
        /// </summary>  
        public virtual async Task UpdateAsync(TDto input)
        {
            var entity = await repository.GetAsync(input.Id);
            var prev = entity.MapTo<TEntity, TEntity>();

            //await repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            await OnBeforeUpdate(entity, input);

            if (entity is IHasManage hasManage)
            {
                var currUser = AppSession.CurrUser;
                hasManage.ModifyTime = DateTime.Now;
                hasManage.Modify_User_Id = currUser?.Id;
            }

            input.MapSet(entity);
            await OnUpdateing(input, entity);

            await repository.UpdateAsync(entity);
            await repository.CommmitAsync();

            if (entity is ITreeEntity treeEntity)
            {
                var type_name = typeof(TEntity).Name;
                var super_id = treeEntity.Super_Id;
                loader.GetService<IBackgroundJob>().SetTimeout<ITreeHandleService>(v => v.MakeTreeCodeAsync(type_name, super_id), null);
            }
        }

        /// <summary>
        /// 验证重复属性
        /// </summary> 
        public virtual async Task<bool> VerifyUnique(UniqueInput input)
        {
            var filters = input
                .KeyValues
                .Select(x => new FieldQueryFilter<TEntity>
                {
                    Compare = "==",
                    Value = x.Value,
                    Name = x.Key
                })
                .Append(new FieldQueryFilter<TEntity>
                {
                    Compare = "!=",
                    Value = input.Id,
                    Name = "Id"
                });


            return await repository.DynamicQuery(filters.ToArray()).AnyAsync();
        }

        /// <summary>
        /// 主查询表达式
        /// </summary> 
        protected override IQueryable<TDto> DefaultQueryable()
        {
            return repository.Queryable.MapTo<TEntity, TDto>();
        }
    }
}
