using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }

    public abstract class BaseController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        private readonly IService<TEntity, TDto> service;

        public Infrastructure.Interface.IScopeServiceLoader ServiceLoader { get; }

        public BaseController(IService<TEntity, TDto> service, Infrastructure.Interface.IScopeServiceLoader serviceLoader)
        {
            this.service = service;
            ServiceLoader = serviceLoader;
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Permission("Add", "添加")]
        [HttpPost]
        public virtual async Task<TDto> Post([FromBody]TDto @input)
        {
            return await service.AddAsync(@input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Permission("Delete", "删除")]
        public virtual async Task Delete(string id)
        {
            await service.DeleteAsync(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut, Permission("Update", "修改")]
        public virtual async Task<TDto> Modify([FromBody]TDto @input)
        {
            return await service.UpdateAsync(@input);
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Permission("Get", "查看")]
        [HttpGet("{id}")]
        public virtual async Task<TDto> Get(string id)
        {
            return await service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        [HttpPost, Permission("List", "列表")]
        public virtual async Task<PageList<TDto>> List([FromBody]PagePara pageInfo)
        {
            return await service.GetListAsync(pageInfo);
        }
    }
}
