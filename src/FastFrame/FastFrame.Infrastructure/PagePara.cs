using EF.Core.Expansion.Dynamic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 查询列表参数
    /// </summary>
    public class PagePara: IPageQueryParameter
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get => _pageIndex <= 0 ? 1 : _pageIndex; set => _pageIndex = value; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get => _pageSize < 0 ? 10 : _pageSize; set => _pageSize = value; }

        /// <summary>
        /// 返回查询到的总记录数量
        /// </summary> 
        public int Total { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public QueryCondition Condition { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public IEnumerable<SortingParameter> Sortings { get; set; }
    } 
}
