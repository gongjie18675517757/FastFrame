﻿using AspectCore.DynamicProxy;
using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public BaseService(IRepository<TEntity> repository, IScopeServiceLoader loader)
        {
            this.repository = repository;
            Loader = loader;
        }

        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual Task OnAdding(TDto input, TEntity entity) => Task.CompletedTask;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AutoCacheInterceptor(AutoCacheOperate.Add)]
        public async Task<TDto> AddAsync(TDto input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var entity = input.MapTo<TDto, TEntity>();
            await repository.AddAsync(entity);
            await OnAdding(input, entity);
            await repository.CommmitAsync();

            return await GetAsync(entity.Id);
        }

        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual Task OnDeleteing(TEntity input) => Task.CompletedTask;


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AutoCacheInterceptor(AutoCacheOperate.Remove)]
        public async Task DeleteAsync(params string[] ids)
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
        }

        protected virtual Task OnGeting(TDto dto) => Task.CompletedTask;

        /// <summary>
        /// 获取单条数据
        /// </summary> 
        [AutoCacheInterceptor(AutoCacheOperate.Get)]
        public async Task<TDto> GetAsync(string id)
        {
            var entity = await Query().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new Exception("ID不正确");
            await OnGeting(entity);
            return entity;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public async Task<PageList<TDto>> GetListAsync(PagePara pageInfo)
        {
            var query = Query().DynamicQuery(pageInfo.Condition);
            return new PageList<TDto>()
            {
                Total = await query.CountAsync(),
                Data = await query.DynamicSort(pageInfo.SortInfo)
                    .Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1))
                    .Take(pageInfo.PageSize)
                    .ToListAsync(),
            };
        }

        /// <summary>
        /// 拼查询表达式
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TDto> Query() => QueryMain();

        /// <summary>
        /// 主查询表达式
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TDto> QueryMain()
        {
            return repository.Queryable.MapTo<TEntity, TDto>();
        }

        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual Task OnUpdateing(TDto input, TEntity entity) => Task.CompletedTask;

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AutoCacheInterceptor(AutoCacheOperate.Update)]
        public async Task<TDto> UpdateAsync(TDto input)
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

            return await GetAsync(input.Id);
        }

        /// <summary>
        /// 验证属性
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> VerifyUnique(UniqueInput input)
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

    public enum AutoCacheOperate
    {
        Get,
        Add,
        Update,
        Remove,
    }

    public class AutoCacheInterceptor : AbstractInterceptorAttribute
    {
        private readonly AutoCacheOperate autoCacheOperate;

        public AutoCacheInterceptor(AutoCacheOperate autoCacheOperate)
        {
            this.autoCacheOperate = autoCacheOperate;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var resultType = context.ProxyMethod.ReturnType;
            resultType = resultType.GetGenericArguments()[0];
            switch (autoCacheOperate)
            {
                case AutoCacheOperate.Get:
                    {
                        var pars = context.Parameters;
                        var key = pars[0].ToString();
                        var value = await RedisHelper.GetAsync(key);
                        if (value.IsNullOrWhiteSpace())
                        {
                            await next(context);
                            var result = await context.UnwrapAsyncReturnValue();
                            var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                            await RedisHelper.SetAsync(((IDto)result).Id, jsonValue, 60 * 60 * 10);
                        }
                        else
                        {
                            var resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject(value, resultType);
                            dynamic member = context.ServiceMethod.ReturnType.GetMember("Result")[0];
                            dynamic temp = System.Convert.ChangeType(resultValue, member.PropertyType);
                            context.ReturnValue = System.Convert.ChangeType(Task.FromResult(temp), context.ServiceMethod.ReturnType);
                            //context.ReturnValue = Task.FromResult(resultValue);
                        }
                    }
                    break;
                case AutoCacheOperate.Add:
                    {
                        await next(context);
                        var result = await context.UnwrapAsyncReturnValue();
                        var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        await RedisHelper.SetAsync(((IDto)result).Id, jsonValue, 60 * 60 * 10);
                    }
                    break;
                case AutoCacheOperate.Update:
                    {
                        await next(context);
                        var result = await context.UnwrapAsyncReturnValue();
                        var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        await RedisHelper.SetAsync(((IDto)result).Id, jsonValue, 60 * 60 * 10);
                    }
                    break;
                case AutoCacheOperate.Remove:
                    {
                        var pars = context.Parameters.Cast<string>().ToArray();
                        await RedisHelper.DelAsync(pars);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
