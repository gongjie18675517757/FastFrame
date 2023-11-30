using FastFrame.Infrastructure.Module;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FastFrame.Infrastructure.Interface
{
    public class ExcelExportProvider(IModuleExportProvider moduleExportProvider, IEnumItemProvider enumItemService) : IExcelExportProvider
    {

        /// <summary>
        /// 生成要导出的列
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public async IAsyncEnumerable<ExcelColumn<TDto>> GenerateExcelColumns<TDto>()
        {
            var moduleStruct = moduleExportProvider.GetModuleStruts(typeof(TDto).Name);
            var enumValues = new Dictionary<int?, IReadOnlyDictionary<int, string>>();

            foreach (var item in moduleStruct.FieldInfoStruts)
            {
                if (item.Hide == "List" || item.Hide == "All")
                    continue;

                if (item.Name == "Id")
                    continue;

                if (item.Name.Contains("password", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                /*数据字典*/
                if (item.EnumItemInfo != null)
                {
                    if (!enumValues.TryGetValue(item.EnumItemInfo, out var values))
                    {
                        values = await enumItemService.EnumValues(item.EnumItemInfo.Value);
                        enumValues.Add(item.EnumItemInfo, values);
                    }

                    yield return new ExcelEnumItemColumn<TDto>(item.Name, item.Description, values);
                }


                /*预定义枚举*/
                else if (item.EnumValues != null && item.EnumValues.Any())
                {
                    yield return new ExcelEnumValuesColumn<TDto>(item.Name, item.Description, item.EnumValues);
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
                            yield return new ExcelBooleanColumn<TDto>(item.Name, item.Description);
                            break;
                        case "DateTime":
                        case "DateOnly":
                        case "TimeOnly":
                            yield return new ExcelDateTimeColumn<TDto>(item.Name, item.Description);
                            break;
                        case "Int32":
                        case "String":
                        case "Decimal":
                        default:
                            yield return new ExcelRawColumn<TDto>(item.Name, item.Description);
                            break;
                    }
                }
            }
        }

        public async Task<byte[]> GenerateExcelSteam<TDto>(Func<IPagination<TDto>, Task<IPageList<TDto>>> pageListFunc,
                                                           IAsyncEnumerable<ExcelColumn<TDto>> columns,
                                                           IPagination<TDto> pagination)
        {
            pagination ??= new Pagination<TDto>()
            {
                PageIndex = 1,
                PageSize = 50
            };


            using var package = new ExcelPackage();
            var sh = package.Workbook.Worksheets.Add("sheet1");

            int rIndex = 1, cIndex = 1;
            await foreach (var column in columns)
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
                    await foreach (var column in columns)
                    {
                        sh.Cells[rIndex, cIndex++].Value = column.GetValue(row);
                    }
                }


                if (pageList.Data.Count < pagination.PageSize)
                    break;

                pagination.PageIndex++;
            }

            SetUsedRangeStyles(sh, rIndex, cIndex);
            sh.View.FreezePanes(2, 1);

            return package.GetAsByteArray();
        }

        private static void SetUsedRangeStyles(ExcelWorksheet sh, int rowEnd, int colEnd)
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
