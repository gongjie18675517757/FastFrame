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
        public virtual async Task<IPageList<TDto>> List([FromQuery]string qs)
        {
            return await service.PageListAsync(Pagination.FromJson(qs));
        }

        [HttpGet]
        public Pagination TestPage()
        {
            return new Pagination
            {
                Filters = new KeyValuePair<FilterMode, IEnumerable<IFilter[]>>[]
                {
                    new KeyValuePair<FilterMode, IEnumerable<IFilter[]>>(FilterMode.and,new IFilter[][]
                    {
                        new IFilter[]
                        {
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                        },
                        new IFilter[]
                        {
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                        }
                    }),
                    new KeyValuePair<FilterMode, IEnumerable<IFilter[]>>(FilterMode.and,new IFilter[][]
                    {
                        new IFilter[]
                        {
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                        },
                        new IFilter[]
                        {
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                            new Filter
                            {
                                Compare="==",
                                Name="xx",
                                Value="xx"
                            },
                        }
                    })
                }
            };
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Permission(new string[] { "List" })]
        [HttpGet]
        public virtual async Task<IActionResult> ExportList(string qs, string fileName)
        {
            var excelExportProvider = Request.HttpContext.RequestServices.GetService<IExcelExportProvider>();
            var columns = await excelExportProvider.GenerateExcelColumns<TDto>().ToListAsync();
            columns = await FmtExportColumns(columns);

            var bytes = await excelExportProvider.GenerateExcelSteam(service.PageListAsync, columns, Pagination.FromJson(qs));
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
        }

        protected Task<List<ExcelColumn<TDto>>> FmtExportColumns(List<ExcelColumn<TDto>> columns)
        {
            return Task.FromResult(columns);
        }
    }
}
