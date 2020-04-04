using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using FastFrame.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers
{
    public abstract class BaseCURDController<TEntity, TDto> : BaseController<TEntity, TDto>
         where TEntity : class, IEntity, new()
         where TDto : class, IDto<TEntity>, new()
    {
        private readonly IService<TEntity, TDto> service;

        public BaseCURDController(IService<TEntity, TDto> service, IScopeServiceLoader serviceLoader) : base(service, serviceLoader)
        {
            this.service = service;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Permission("Add", "添加")]
        [HttpPost]
        public virtual async Task<string> Post([FromBody]TDto @input)
        {
            return await service.AddAsync(@input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Permission("Update", "修改")]
        public virtual async Task Put([FromBody]TDto @input)
        {
            await service.UpdateAsync(@input);
        }

    }
}
