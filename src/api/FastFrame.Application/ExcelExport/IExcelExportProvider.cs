using FastFrame.Infrastructure;

namespace FastFrame.Application.ExcelExport
{
    /// <summary>
    /// EXCEL导出
    /// </summary>
    public interface IExcelExportProvider
    {
        /// <summary>
        /// 生成要导出的列
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        IAsyncEnumerable<ExcelColumn<TDto>> GenerateExcelColumns<TDto>();

        /// <summary>
        /// 生成EXCEL流
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="pageListFunc"></param>
        /// <param name="columns"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<byte[]> GenerateExcelSteam<TDto>(Func<IPagination<TDto>, Task<IPageList<TDto>>> pageListFunc,
                                            IEnumerable<ExcelColumn<TDto>> columns,
                                            IPagination<TDto> pagination)
        {
            return GenerateExcelSteam<TDto>(pageListFunc, columns.AsAsyncEnumerable(), pagination);
        }

        /// <summary>
        /// 生成EXCEL流
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="pageListFunc"></param>
        /// <param name="columns"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<byte[]> GenerateExcelSteam<TDto>(Func<IPagination<TDto>, Task<IPageList<TDto>>> pageListFunc,
                                            IAsyncEnumerable<ExcelColumn<TDto>> columns,
                                            IPagination<TDto> pagination);


    }
}
