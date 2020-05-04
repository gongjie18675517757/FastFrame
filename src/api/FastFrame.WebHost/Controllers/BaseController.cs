using FastFrame.Application;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {

    }

    public abstract class BaseController<TDto> : BaseController
        where TDto : class, IDto, new()
    {
        private readonly IPageListService<TDto> service;

        public BaseController(IPageListService<TDto> service)
        {
            this.service = service;
        }

        /// <summary>
        /// 列表
        /// </summary> 
        [HttpGet]
        [Permission("List", "列表")]
        public virtual async Task<PageList<TDto>> List(string qs)
        {
            return await service.PageListAsync(qs.ToObject<Pagination>());
        }
    }
}
