using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
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
        public IScopeServiceLoader ServiceLoader { get; }

        public BaseController(IService<TEntity, TDto> service, IScopeServiceLoader serviceLoader)
        {
            this.service = service;
            ServiceLoader = serviceLoader;
        }


       

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Permission("Delete", "删除")]
        public virtual async Task Delete(string id)
        {
            await service.DeleteAsync(id);
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
        [HttpPost] 
        [Permission("List", "列表")]
        public virtual async Task<PageList<TDto>> List(PagePara pageInfo)
        {
            return await service.GetListAsync(pageInfo);
        }
    }
}
