using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 查询列表参数
    /// </summary>
    public class Pagination
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
        /// 模糊条件
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public List<KeyValuePair<string, List<Filter>>> Filters { get; set; }
            = new List<KeyValuePair<string, List<Filter>>>();

        /// <summary>
        /// 排序列名称
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string SortMode { get; set; }
    }


    public class Filter
    {
        public string Name { get; set; }

        public string Compare { get; set; }

        public string Value { get; set; }
    }
}
