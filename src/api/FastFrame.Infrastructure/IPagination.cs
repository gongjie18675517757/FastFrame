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
        /// 条件
        /// </summary>
        IEnumerable<KeyValuePair<FilterMode, IEnumerable<IFilter[]>>> Filters { get; set; }

        /// <summary>
        /// 模糊条件
        /// </summary>
        string KeyWord { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        string SortMode { get; set; }

        /// <summary>
        /// 排序列名称
        /// </summary>
        string SortName { get; set; }

        /// <summary>
        /// 尝试替换条件
        /// 如果返回为null,则删除
        /// </summary>
        /// <param name="func"></param>
        /// <param name="replaceFunc"></param>
        void TryReplaceFilter<TFilter>(Func<TFilter, bool> func, Func<TFilter, IFilter> replaceFunc)
        {
            if (Filters == null || !Filters.Any())
                return;

            var filtersArr = Filters.ToArray();

            for (int kvIndex = 0; kvIndex < filtersArr.Length; kvIndex++)
            {
                var kv = filtersArr[kvIndex]; 

                foreach (var brr in kv.Value)
                {
                    for (int x = 0; x < brr.Length; x++)
                    {
                        var f = brr[x];
                        if (f is TFilter filter)
                        {
                            if (func(filter))
                            {
                                brr[x] = replaceFunc(filter);
                            }
                        }
                    }

                }
            }
        }
    }

}
