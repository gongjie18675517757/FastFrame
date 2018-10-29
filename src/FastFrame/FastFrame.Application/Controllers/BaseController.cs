using FastFrame.Dto;
using FastFrame.Entity;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Attrs;
using FastFrame.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FastFrame.Application.Controllers
{
    public abstract class BaseController : Controller
    {
    }


    public abstract class BaseController<TEntity, TDto> : BaseController
        where TEntity : class, IEntity, new()
        where TDto : class, IDto<TEntity>, new()
    {
        public BaseController(IService<TEntity, TDto> service)
        {
            this.Service = service;
        }

        public IService<TEntity, TDto> Service { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Permission("Add", "添加")]
        [HttpPost]
        public async Task<TDto> Post([FromBody]TDto @input)
        {
            return await Service.AddAsync(@input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Permission("Delete", "删除")]
        public async Task Delete([FromQuery]string id)
        {
            await Service.DeleteAsync(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut, Permission("Update", "修改")]
        public async Task<TDto> Modify([FromBody]TDto @input)
        {
            return await Service.UpdateAsync(@input);
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Permission("Get", "查看")]
        public async Task<TDto> Get([FromQuery]string id)
        {
            return await Service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        [HttpPost, Permission("List", "列表")]
        public async Task<PageList<TDto>> List([FromBody]PagePara pageInfo)
        {
            return await Service.GetListAsync(pageInfo);
        }
    }
}
