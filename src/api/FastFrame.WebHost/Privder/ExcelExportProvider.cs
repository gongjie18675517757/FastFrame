using FastFrame.Application.Basis;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure.Module;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Privder
{
    public class ExcelExportProvider : IExcelExportProvider
    {
        private readonly IModuleExportProvider moduleExportProvider;
        private readonly EnumItemService enumItemService;

        public ExcelExportProvider(IModuleExportProvider moduleExportProvider, EnumItemService enumItemService)
        {
            this.moduleExportProvider = moduleExportProvider;
            this.enumItemService = enumItemService;
        }

        /// <summary>
        /// 生成要导出的列
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public async IAsyncEnumerable<ExcelColumn<TDto>> GenerateExcelColumns<TDto>()
        {
            var moduleStruct = moduleExportProvider.GetModuleStruts(typeof(TDto).Name);
            var enumValues = new Dictionary<EnumName, IEnumerable<EnumItemModel>>();

            foreach (var item in moduleStruct.FieldInfoStruts)
            {
                if (item.Hide == Infrastructure.Attrs.HideMark.List || item.Hide == Infrastructure.Attrs.HideMark.All)
                    continue;

                if (item.Name == "Id")
                    continue;

                if (item.Name.ToLower().Contains("password"))
                    continue;

                /*数据字典*/
                if (item.EnumItemInfo != null && !item.EnumItemInfo.Name.IsNullOrWhiteSpace())
                {
                    if (Enum.TryParse<EnumName>(item.EnumItemInfo.Name, out var enumName))
                    {
                        if (!enumValues.TryGetValue(enumName, out var values))
                        {
                            values = await enumItemService.GetValues(enumName);
                            enumValues.Add(enumName, values);
                        }

                        yield return new ExcelColumn<TDto>
                        {
                            Title = item.Description,
                            ValueFunc = model => string.Join(",",
                                            values.Where(v => model.GetValue(item.Name)?.ToString().Contains(v.Id) == true).Select(v => v.Value))
                        };
                    }
                }

                /*关联表*/
                else if (item.Relate != null)
                {
                    var relateModuleStruct = moduleExportProvider.GetModuleStruts(item.Relate);
                    var relateFields = relateModuleStruct.RelateFields;

                    yield return new ExcelColumn<TDto>
                    {
                        Title = item.Description,
                        ValueFunc = model =>
                        {
                            if (model == null)
                                return null;

                            var obj = model.GetValue(item.Name.Replace("_Id", ""));

                            if (obj == null)
                                return null;


                            return string.Join("", relateFields
                                                 .Select(v => obj.GetValue(v))
                                                 .Where(v => v != null)
                                                 .Select((v, i) => i == 0 ? v.ToString() : $"[{v}]"));
                        }

                    };
                }

                /*预定义枚举*/
                else if (item.EnumValues.Any())
                {
                    yield return new ExcelColumn<TDto>
                    {
                        Title = item.Description,
                        ValueFunc = model => string.Join(",",
                                        item.EnumValues.Where(v => model.GetValue(item.Name)?.ToString().Contains(v.Key) == true).Select(v => v.Value))
                    };
                }

                /*未匹配到的ID字段*/
                else if (item.Name.EndsWith("_Id"))
                {
                    continue;
                }

               


                /*值类型*/
                else
                {
                    switch (item.Type)
                    {
                        case "Boolean":
                            yield return new ExcelColumn<TDto>
                            {
                                Title = item.Description,
                                ValueFunc = model => model == null ?
                                                        null : (bool)model.GetValue(item.Name) ?
                                                        "是" : "否"
                            };
                            break;
                        case "DateTime":
                            yield return new ExcelColumn<TDto>
                            {
                                Title = item.Description,
                                ValueFunc = model => model == null ?
                                                        null : ((DateTime)model.GetValue(item.Name)).ToString("yyyy-MM-dd")
                            };
                            break;
                        case "Int32":
                        case "String":
                        case "Decimal":
                            yield return new ExcelColumn<TDto>
                            {
                                Title = item.Description,
                                ValueFunc = model => model?.GetValue(item.Name)
                            };
                            break;
                        default:

                            break;
                    }
                }
            }
        }

        public async Task<byte[]> GenerateExcelSteam<TDto>(Func<Pagination, Task<PageList<TDto>>> pageListFunc, IEnumerable<ExcelColumn<TDto>> columns, Pagination pagination)
        {
            pagination ??= new Pagination();
            pagination.PageIndex = 1;
            pagination.PageSize = 50;

            using var package = new ExcelPackage();
            var sh = package.Workbook.Worksheets.Add("sheet1");

            int rIndex = 1, cIndex = 1;
            foreach (var column in columns)
            {
                sh.Cells[rIndex, cIndex++].Value = column.Title;
            }

            while (true)
            {
                var pageList = await pageListFunc(pagination);

                foreach (var row in pageList.Data)
                {
                    rIndex++;
                    cIndex = 1;
                    foreach (var column in columns)
                    {
                        sh.Cells[rIndex, cIndex++].Value = column.ValueFunc(row);
                    }
                }


                if (pageList.Data.Count < pagination.PageSize)
                    break;
            }

            SetUsedRangeStyles(sh, rIndex, cIndex);

            return package.GetAsByteArray();
        }

        private void SetUsedRangeStyles(ExcelWorksheet sh, int rowEnd, int colEnd)
        {
            var usedRng = sh.Cells[1, 1, rowEnd, colEnd];
            usedRng.AutoFitColumns(10, 50);
            foreach (var r in usedRng)
            {
                r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
            }
        }
    }
}
