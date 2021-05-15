using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace FastFrame.Application
{
    public static class ExpandMethod
    {
        public static async Task<PageList<T>> PageListAsync<T>(this IQueryable<T> query, Pagination pageInfo)
        {
            pageInfo ??= new Pagination();
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


        public static IQueryable<T> DynamicSort<T>(this IQueryable<T> query, string name, string mode)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Id";
            }

            if (string.IsNullOrWhiteSpace(mode))
            {
                mode = "desc";
            }

            return query.OrderBy($"{name} {mode}");
        }


        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, string kw, List<KeyValuePair<string, List<Filter>>> kvsFilter)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var isExistsFilter = !kw.IsNullOrWhiteSpace();
            isExistsFilter = isExistsFilter ||
                                (kvsFilter != null &&
                                    kvsFilter.Any(v => v.Value != null &&
                                                       v.Key != null &&
                                                        v.Value.Any(r => r.Name != null && r.Value != null && r.Compare != null)));

            if (!isExistsFilter)
                return query;

            foreach (var kv in kvsFilter)
            {
                foreach (var v in kv.Value)
                {
                    if (v.Value?.Trim().ToLower() == "null")
                        v.Value = null;
                }
            }

            if (!kw.IsNullOrWhiteSpace())
            {
                var props = typeof(T).GetProperties()
                    .Where(x => x.PropertyType == typeof(string) && !x.Name.EndsWith("Id"));
                query = query.Where(string.Join(" or ", props.Select(x => $"{x.Name}.Contains(@0)")), kw);
            }

            var rIndex = 0;
            var condList = new List<(string key, List<string> strList, List<object> valList)>();
            foreach (var kv in kvsFilter)
            {
                var v = CalcFilter<T>(ref rIndex, kv);
                condList.Add(v);
            }

            if (condList.Count > 0)
            {
                var stringBuilder = new StringBuilder(" 1=1 ");
                var queryParList = new List<object>();
                foreach (var (key, strList, valList) in condList)
                {
                    stringBuilder.Append(key);
                    stringBuilder.Append(" ( (");
                    stringBuilder.Append(string.Join(") and (", strList));
                    stringBuilder.Append(") ) ");

                    queryParList.AddRange(valList);
                }

                var queryStr = stringBuilder.ToString();

                query = query.Where(queryStr, queryParList.ToArray());
            }

            return query;
        }

        public static PropertyInfo GetPropertyInfoByNames(Type type, string propNamePath)
        {
            var names = propNamePath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var propertyInfo = type.GetProperty(names[0]);
            if (propertyInfo == null)
                return null;

            foreach (var name in names.Skip(1))
            {
                propertyInfo = propertyInfo.PropertyType.GetProperty(name);
                if (propertyInfo == null)
                    return null;
            }

            return propertyInfo;
        }

        private static (string key, List<string> strList, List<object> valList) CalcFilter<T>(ref int rIndex, KeyValuePair<string, List<Filter>> kvs)
        {
            var filters = kvs.Value;
            var queryStrList = new List<string>();
            var queryValList = new List<object>();
            foreach (var item in filters)
            {
                var conds = item.Name.Split(";,".ToArray(), StringSplitOptions.RemoveEmptyEntries);

                if (!conds.Any())
                    continue;

                if (item.Compare.ToLower() == "$")
                {
                    var values = item.Value.ToSplitArray(" ");
                    if (values.Length == 0)
                        continue;


                    var qList = new List<string>();

                    foreach (var value in values)
                    {
                        foreach (var cond in conds)
                        {
                            qList.Add($"{cond}.Contains(@{rIndex})");
                        }
                        rIndex++;
                    }
                    var queryStr = string.Join(" or ", qList);
                    queryStrList.Add(queryStr);
                    queryValList.AddRange(values);
                }
                else if (item.Compare.ToLower().EndsWith("in"))
                {
                    var values = item.Value.ToSplitArray(",;");
                    if (values.Length == 0)
                        continue;

                    var propertyInfo = GetPropertyInfoByNames(typeof(T), item.Name);
                    var operate = item.Compare.StartsWith("!") ? "!=" : "==";

                    /*如果属性为数组，则要拆出来成多个OR*/
                    if (propertyInfo.PropertyType.IsArray)
                    {
                        var qList = new List<string>();

                        foreach (var v in values)
                        {
                            qList.Add($"{item.Name}.Contains(@{rIndex++})");
                        }
                        var queryStr = string.Join(" or ", qList);
                        queryStrList.Add(queryStr);
                        queryValList.AddRange(values);
                    }
                    else
                    {
                        queryStrList.Add($"{operate}@{rIndex++}.Contains({item.Name})");
                        queryValList.Add(values);
                    }
                }
                else
                {
                    var index = rIndex;
                    var queryStr = string.Join(" or ", conds.Select((r) => $"{r} {item.Compare} @{index}"));
                    queryStrList.Add(queryStr);
                    queryValList.Add(item.Value);
                    rIndex++;
                }
            }

            return (kvs.Key, queryStrList, queryValList);
        }
    }
}
