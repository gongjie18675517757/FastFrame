using AspectCore.Injector;
using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.MessageBus;
using FastFrame.Repository;
using FastFrame.Service.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FastFrame.Service
{
    /// <summary>
    /// 服务层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public class BaseService<TEntity, TDto> : IService<TEntity, TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        private readonly IRepository<TEntity> repository;

        public IScopeServiceLoader Loader { get; }

        [FromContainer]
        public IEventBus EventBus { get; set; }

        [FromContainer]
        public IClientManage ClientManage { get; set; }

        public BaseService(IRepository<TEntity> repository, IScopeServiceLoader loader)
        {
            this.repository = repository;
            Loader = loader;
        }

        /// <summary>
        /// 新增前
        /// </summary> 
        protected virtual Task OnAdding(TDto input, TEntity entity)
            => EventBus?.TriggerAsync(new DoMainAdding<TDto>(input));

        /// <summary>
        /// 新增
        /// </summary> 
        [AutoCacheInterceptor(AutoCacheOperate.Add)]
        public virtual async Task<TDto> AddAsync(TDto input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var entity = input.MapTo<TDto, TEntity>();
            await repository.AddAsync(entity);
            input.Id = entity.Id;
            await OnAdding(input, entity);
            await repository.CommmitAsync();

            var dto = await GetAsync(entity.Id);
            await EventBus?.TriggerAsync(new DoMainAdded<TDto>(dto));

            await ClientManage.SendAsync(
                new DoMainMessage<TDto>(typeof(TEntity).Name, MsgType.DataAdded, dto)
            );

            return dto;
        }

        /// <summary>
        /// 删除前
        /// </summary> 
        protected virtual Task OnDeleteing(TEntity input)
            => EventBus?.TriggerAsync(new Events.DoMainDeleteing<TDto>(input.Id));


        /// <summary>
        /// 删除
        /// </summary> 
        [AutoCacheInterceptor(AutoCacheOperate.Remove)]
        public virtual async Task DeleteAsync(params string[] ids)
        {
            foreach (var id in ids)
            {
                var entity = await repository.GetAsync(id);
                await OnDeleteing(entity);
                if (entity == null)
                    throw new Exception("ID不正确");
                await repository.DeleteAsync(entity);
            }
            await repository.CommmitAsync();
            foreach (var id in ids)
            {
                await EventBus?.TriggerAsync(new Events.DoMainDeleted<TDto>(id));
                await ClientManage.SendAsync(
                    new DoMainMessage<string>(typeof(TEntity).Name, MsgType.DataDeleted, id)
              );
            }
        }

        /// <summary>
        /// 更新前
        /// </summary> 
        protected virtual Task OnUpdateing(TDto input, TEntity entity)
            => EventBus?.TriggerAsync(new Events.DoMainUpdateing<TDto>(input));

        /// <summary>
        /// 更新
        /// </summary> 
        [AutoCacheInterceptor(AutoCacheOperate.Update)]
        public virtual async Task<TDto> UpdateAsync(TDto input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            var entity = await repository.GetAsync(input.Id);
            input.MapSet(entity);
            await OnUpdateing(input, entity);
            await repository.UpdateAsync(entity);
            await repository.CommmitAsync();
            await RedisHelper.DelAsync(input.Id);
            var dto = await GetAsync(input.Id);
            await EventBus?.TriggerAsync(new DoMainUpdated<TDto>(dto));

            await ClientManage.SendAsync(
                new DoMainMessage<TDto>(typeof(TEntity).Name, MsgType.DataUpdated, dto)
            );
            return dto;
        }

        /// <summary>
        /// 返回前
        /// </summary> 
        protected virtual Task OnGeting(TDto dto)
            => EventBus.TriggerAsync(new DoMainResulting<TDto>(dto));

        /// <summary>
        /// 获取单条数据
        /// </summary> 
        //[AutoCacheInterceptor(AutoCacheOperate.Get)]
        public virtual async Task<TDto> GetAsync(string id)
        {
            var dto = (await Query().Where("Id=@0", id).ToListAsync()).FirstOrDefault();
            if (dto != null)
                await OnGeting(dto);
            return dto;
        }

        protected virtual IQueryable<TDto> GetListQueryableing(IQueryable<TDto> query) => query;

        /// <summary>
        /// 返回列表数据时
        /// </summary> 
        protected virtual Task OnGetListing(IEnumerable<TDto> dtos)
            => EventBus.TriggerAsync(new DoMainResultListing<TDto>(dtos));

        /// <summary>
        /// 获取分页列表
        /// </summary> 
        public virtual async Task<PageList<TDto>> GetListAsync(PagePara pageInfo)
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
            return await repository.Queryable.DynamicQuery(new QueryCondition()
            {
                Filters = filters
            }).AnyAsync();
        }
    }
}
