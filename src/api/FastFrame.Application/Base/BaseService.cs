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
        /// 构建主查询表达式
        /// </summary>
        /// <returns></returns>
        internal virtual IQueryable<TDto> BuildQuery() => DefaultQueryable();

        /// <summary>
        /// 代码生成器生成的查询表达式
        /// </summary> 
        protected abstract IQueryable<TDto> DefaultQueryable();

        /// <summary>
        /// 查询view model
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="kw"></param>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IViewModel>> ViewModelListAsync<TEntity>(string kw, int page_index = 1, int page_size = 10)
            where TEntity : class, IViewModelable<TEntity>
        {
            var query = loader.GetService<IRepository<TEntity>>().Select(TEntity.BuildExpression());

            var list= await query
                .Where(v => kw == null || v.Value.Contains(kw))
                .OrderByDescending(v => v.Id)
                .Skip(page_size * (page_index - 1))
                .Take(page_size)
                .ToListAsync();

            return list;
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
        private readonly IRepository<TEntity> repository;

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


            var addEvent = new DoMainAdded<TDto> { Id = entity.Id };
            loader.GetService<IBackgroundJob>().SetTimeout<IEventBus>(v => v.TriggerEventAsync(addEvent), null);

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
            }
            await repository.CommmitAsync();

            foreach (var entity in entitys)
            {
                var delEvent = new DoMainDeleted<TDto>() { Id = entity.Id };
                loader.GetService<IBackgroundJob>().SetTimeout<IEventBus>(v => v.TriggerEventAsync(delEvent), null);

                if (entity is ITreeEntity treeEntity)
                {
                    /*有下级，则不可删除*/
                    var expression = ExpressionClosureFactory.BuildEqualExpression<TEntity, string>(nameof(ITreeEntity.Super_Id), entity.Id);
                    if (await repository.AnyAsync(expression))
                        throw new MsgException($"{treeEntity}不可删除,因为他还有下级");
                }
            }
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

            if (entity is ITreeEntity treeEntity && prev is ITreeEntity prevTreeEntity)
            {
                /*有下级，则不可修改上级关系*/
                if (treeEntity.Super_Id != prevTreeEntity.Super_Id)
                {
                    var expression = ExpressionClosureFactory.BuildEqualExpression<TEntity, string>(nameof(ITreeEntity.Super_Id), entity.Id);
                    if (await repository.AnyAsync(expression))
                        throw new MsgException($"{treeEntity}不可修改上级关系,因为他还有下级");
                }

                /*重新编码*/
                if (treeEntity.Super_Id != prevTreeEntity.Super_Id || treeEntity.TreeCode.IsNullOrWhiteSpace())
                    await loader.GetService<IAutoNumberService>().TryMakeNumberAsync(entity);
            }


            await repository.UpdateAsync(entity);
            await repository.CommmitAsync();

            var updateEvent = new DoMainUpdated<TDto> { Id = entity.Id };
            loader.GetService<IBackgroundJob>().SetTimeout<IEventBus>(v => v.TriggerEventAsync(updateEvent), null);
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
