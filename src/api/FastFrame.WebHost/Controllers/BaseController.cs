using AspectCore.DependencyInjection;
using FastFrame.Application;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        [FromServiceContext]
        protected IAppSessionProvider AppSession { get; set; }
    }

    public abstract class BaseController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        private readonly IService<TEntity, TDto> service;

        public BaseController(IService<TEntity, TDto> service)
        {
            this.service = service;
        } 
       

        /// <summary>
        /// 列表
        /// </summary> 
        [HttpPost]
        [Permission("List", "列表")]
        public virtual async Task<PageList<TDto>> List(Pagination pageInfo)
        {
            return await service.GetListAsync(pageInfo);
        }

        /// <summary>
        /// 列表
        /// </summary> 
        [HttpGet("{pageIndex}")]
        [Permission(new string[] { "List" })]
        public virtual async Task<PageList<TDto>> List(int pageIndex, int pageSize = 10, string kw = null)
        {
            return await List(new Pagination
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                KeyWord = kw
            });
        } 
    }
}
