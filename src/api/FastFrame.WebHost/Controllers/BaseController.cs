using FastFrame.Application;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;

namespace FastFrame.WebHost.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {

    }

    public abstract class BaseController<TDto>(IPageListService<TDto> service) : BaseController
        where TDto : class
    {
        private readonly IPageListService<TDto> service = service;

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
        [HttpGet]
        [Permission("List", "列表")]
        public virtual async Task<IPageList<TDto>> List([FromQuery]string qs)
        {
            return await service.PageListAsync(Pagination<TDto>.FromJson(qs));
        } 

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Permission(["List"])]
        [HttpGet]
        public virtual async Task<IActionResult> ExportList(string qs, string fileName)
        {
            var excelExportProvider = Request.HttpContext.RequestServices.GetService<IExcelExportProvider>();
            var columns = await excelExportProvider.GenerateExcelColumns<TDto>().ToListAsync();
            columns = await FmtExportColumns(columns);

            var bytes = await excelExportProvider.GenerateExcelSteam(service.PageListAsync, columns, Pagination<TDto>.FromJson(qs));

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
        }

        /// <summary>
        /// 格式化导出的列
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        protected Task<List<ExcelColumn<TDto>>> FmtExportColumns(List<ExcelColumn<TDto>> columns)
        {
            return Task.FromResult(columns);
        }
    }
}
