using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 查询列表参数
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// 模糊条件
        /// </summary>
        string KeyWord { get; }

        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 排序方式
        /// </summary>
        SortModeEnum SortMode { get; }

        /// <summary>
        /// 排序列名称
        /// </summary>
        string SortName { get; }
    }

    public interface IPagination<TQueryModel> : IPagination
    {
        /// <summary>
        /// 条件
        /// </summary>
        IQueryFilterCollection<TQueryModel> Filters { get;   } 
    }

    public enum SortModeEnum
    {
        /// <summary>
        /// 正序
        /// </summary>
        asc,

        /// <summary>
        /// 倒序
        /// </summary>
        desc,
    }
}
