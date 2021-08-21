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

namespace FastFrame.Application
{
    /// <summary>
    /// 服务层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public class BaseService<TEntity, TDto> : ICURDService<TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        private readonly IRepository<TEntity> repository;

        [FromServiceContext]
        protected IServiceProvider Loader { get; set; }

        [FromServiceContext]
        protected IEventBus EventBus { get; set; }

        [FromServiceContext]
        protected IApplicationSession AppSession { get; set; }

        public BaseService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 新增前
        /// </summary> 
        protected virtual async Task OnAdding(TDto input, TEntity entity)
        {
            if (entity is IHaveNumber haveNumber)
                await EventBus.TriggerEventAsync(new EntityAdding<IHaveNumber>(haveNumber, entity));

            if (input is IHaveMultiFileDto haveMultiFile)
                await EventBus.TriggerEventAsync(new DoMainAdding<IHaveMultiFileDto>(haveMultiFile, entity));

            await EventBus?.TriggerEventAsync(new DoMainAdding<TDto>(input));
        }

        /// <summary>
        /// 新增
        /// </summary>  
        public virtual async Task<string> AddAsync(TDto input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

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

            await EventBus?.TriggerEventAsync(new DoMainAdded<TDto>(input));

            return entity.Id;
        }

        /// <summary>
        /// 删除前
        /// </summary> 
        protected virtual async Task OnDeleteing(TEntity entity)
        {
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
            }
            await repository.CommmitAsync();

            foreach (var entity in entitys)
            {
                await EventBus?.TriggerEventAsync(new DoMainDeleted<TDto>(entity.Id, entity));
            }
        }

        /// <summary>
        /// 更新时
        /// </summary> 
        protected virtual async Task OnUpdateing(TDto input, TEntity entity)
        {
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
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            var entity = await repository.GetAsync(input.Id);

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
            await EventBus?.TriggerEventAsync(new DoMainUpdated<TDto>(input));
        }

        /// <summary>
        /// 返回前
        /// </summary> 
        protected virtual async Task OnGeting(TDto output)
        {
            if (output is IHaveMultiFileDto haveMultiFile)
                haveMultiFile.Files = await EventBus.RequestAsync<IEnumerable<ResourceModel>, IHaveMultiFileDto>(haveMultiFile);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>  
        [AutoCacheInterceptor(AutoCacheOperate.Get)]
        public virtual async Task<TDto> GetAsync(string id)
        {
            if (id.IsNullOrWhiteSpace())
                throw new NotFoundException();

            var dto = await Query().Where(v => v.Id == id).SingleOrDefaultAsync();

            if (dto == null)
                throw new NotFoundException();

            await OnGeting(dto);
            return dto;
        }

        protected virtual IQueryable<TDto> GetListQueryableing(IQueryable<TDto> query) => query;

        /// <summary>
        /// 返回列表数据时
        /// </summary> 
        protected virtual async Task OnGetListing(IEnumerable<TDto> dtos)
        {
            if (typeof(IHaveMultiFileDto).IsAssignableFrom(typeof(TDto)))
            {
                var haveMultiFiles = dtos.Cast<IHaveMultiFileDto>();
                var valuePairs = await EventBus.RequestAsync<IEnumerable<KeyValuePair<string, IEnumerable<ResourceModel>>>, IEnumerable<IHaveMultiFileDto>>(haveMultiFiles);
                foreach (var fileDto in haveMultiFiles)
                {
                    fileDto.Files = valuePairs.Where(v => v.Key == fileDto.Id).SelectMany(v => v.Value);
                }
            }
        }

        /// <summary>
        /// 获取分页列表
        /// </summary> 
        public virtual async Task<PageList<TDto>> PageListAsync(Pagination pageInfo)
        {
            var query = GetListQueryableing(Query());
            var pageList = await query.PageListAsync(pageInfo);

            await OnGetListing(pageList.Data);
            return pageList;
        }

        /// <summary>
        /// 拼查询表达式
        /// </summary>
        /// <returns></returns>
        internal virtual IQueryable<TDto> Query() => QueryMain();

        /// <summary>
        /// 主查询表达式
        /// </summary> 
        protected virtual IQueryable<TDto> QueryMain()
        {
            return repository.Queryable.MapTo<TEntity, TDto>();
        }

        /// <summary>
        /// 验证重复属性
        /// </summary> 
        public virtual async Task<bool> VerifyUnique(UniqueInput input)
        {
            var filters = input.KeyValues.Select(x => new Filter()
            {
                Compare = "==",
                Value = x.Value,
                Name = x.Key
            }).ToList();
            filters.Add(new Filter()
            {
                Compare = "!=",
                Value = input.Id,
                Name = "Id"
            });
            return await repository.Queryable.DynamicQuery(null, new List<KeyValuePair<string, List<Filter>>>
            {
                new KeyValuePair<string, List<Filter>>("and",filters)
            }).AnyAsync();
        }
    }
}
