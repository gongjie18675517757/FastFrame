using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Application
{
    public static class ExpandMethod
    {
        public static async Task<PageList<T>> PageListAsync<T>(this IQueryable<T> query, Pagination pageInfo)
        {
            pageInfo = pageInfo ?? new Pagination();
            query = query.DynamicQuery(pageInfo.KeyWord, pageInfo.Filters);

            var list = await query.DynamicSort(pageInfo.SortName, pageInfo.SortMode)
                    .Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1))
                    .Take(pageInfo.PageSize)
                    .ToListAsync();

            return new PageList<T>()
            {
                Total = await query.CountAsync(),
                Data = list,
            };
        }
    }
}
