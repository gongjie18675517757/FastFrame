using AspectCore.DependencyInjection;
using FastFrame.Application;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
        private readonly IService<TDto> service;

        public BaseController(IService<TDto> service)
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
        public virtual async Task<PageList<TDto>> List(int pageIndex,
                                                       int pageSize = 10,
                                                       string kw = null,
                                                       string sortName = null,
                                                       string sortMode = null,
                                                       string filterStr = null)
        {
            var pagination = new Pagination()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                KeyWord = kw
            };

            if (!sortName.IsNullOrWhiteSpace())
                pagination.SortName = sortName;

            if (!sortMode.IsNullOrWhiteSpace())
                pagination.SortMode = sortMode;

            if (!filterStr.IsNullOrWhiteSpace())
                pagination.Filters = filterStr.ToObject<List<KeyValuePair<string, List<Filter>>>>();

            return await List(pagination);
        }
    }
}
