using FastFrame.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace FastFrame.Application
{
    /// <summary>
    /// 查询的扩展方法
    /// </summary>
    public static class ExpandMethod
    {
        public static async Task<IPageList<T>> PageListAsync<T>(this IQueryable<T> query, IPagination<T> pageInfo)
        {
            pageInfo ??= new Pagination<T>();
            query = query.DynamicQuery(pageInfo.KeyWord, pageInfo.Filters.QueryFilters.ToArray());

            var list = await query.DynamicSort(pageInfo.SortName, pageInfo.SortMode.ToString())
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

        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, params IQueryFilter<T>[] filters)
        {
            return DynamicQuery<T>(query, null, filters);
        }

        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, string kw, params IQueryFilter<T>[] kvsFilter)
        {
            foreach (var kv in kvsFilter)
                query = query.Where(kv.MakePredicate());

            if (!kw.IsNullOrWhiteSpace())
            {
                var props = typeof(T)
                    .GetProperties()
                    .Where(x => x.PropertyType == typeof(string) && !x.Name.EndsWith("Id"));

                query = query.Where(string.Join(" or ", props.Select(x => $"{x.Name}.Contains(@0)")), kw);
            }

            return query;
        }
    }

    /// <summary>
    /// 单字段条件
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class FieldQueryFilter<TQueryModel> : IQueryFilter<TQueryModel>
    {
        private static HashSet<string> fields = new();

        public Expression<Func<TQueryModel, bool>> MakePredicate()
        {
            return MakePredicate(Name, Compare, Value);
        }

        public bool ExistsIsEnabled()
        {
            if (fields.Count == 0)
            {
                fields = typeof(TQueryModel).GetProperties().Select(v => v.Name.ToLower()).ToHashSet();
            }

            return !Name.IsNullOrWhiteSpace() &&
                   !Compare.IsNullOrWhiteSpace() &&
                   !Value.IsNullOrWhiteSpace() &&
                   fields.Contains(Name.ToLower());
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 比较方式
        /// </summary>
        public string Compare { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// 默认过滤表达式
        /// </summary>
        private static Expression<Func<TQueryModel, bool>> DefaultPredicate => v => true;

        /// <summary>
        /// 返回多级属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propNamePath"></param>
        /// <returns></returns>
        private static PropertyInfo GetPropertyInfoByNames(Type type, string propNamePath)
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

        /// <summary>
        /// 生成条件表达式
        /// </summary> 
        private static Expression<Func<TQueryModel, bool>> MakePredicate(string name, string compare, string value)
        {
            if (name.IsNullOrWhiteSpace() ||
                compare.IsNullOrWhiteSpace())
                return DefaultPredicate;

            var conds = name.Split(";,".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            if (!conds.Any())
                return DefaultPredicate;

            var rIndex = 0;
            var queryStr = string.Empty;
            var values = Array.Empty<object>();

            if (compare == "$")
            {
                values = value.ToSplitArray(" ");
                if (values.Length == 0)
                    return DefaultPredicate;


                var qList = new List<string>();

                foreach (var v in values)
                {
                    foreach (var cond in conds)
                        qList.Add($"{cond}.Contains(@{rIndex})");

                    rIndex++;
                }
                queryStr = string.Join(" or ", qList);
            }
            else if (compare.ToLower().EndsWith("in"))
            {
                values = value.ToSplitArray(",;");
                if (values.Length == 0)
                    return DefaultPredicate;

                var propertyInfo = GetPropertyInfoByNames(typeof(TQueryModel), name);
                var operate = compare.StartsWith("!") ? "!=" : "==";

                var propType = T4Help.GetNullableType(propertyInfo.PropertyType);
                if (propType.IsEnum)
                {
                    values = values.Select(v => Enum.Parse(propType, v.ToString())).ToArray();
                }

                /*如果属性为数组，则要拆出来成多个OR*/
                if (propertyInfo.PropertyType.IsArray)
                {
                    var qList = new List<string>();

                    foreach (var v in values)
                        qList.Add($"{name}.Contains(@{rIndex++})");

                    queryStr = string.Join(" or ", qList);
                }
                else
                {
                    var qList = new List<string>();

                    foreach (var v in values)
                        qList.Add($"{name}==(@{rIndex++})");

                    queryStr = string.Join(" or ", qList);
                }
            }
            else
            {
                var index = rIndex;
                queryStr = string.Join(" or ", conds.Select((r) => $"{r} {compare} @{index}"));

                values = new string[] { value };
            }

            return DynamicExpressionParser.ParseLambda<TQueryModel, bool>(ParsingConfig.Default, false, queryStr, values: values);
        }
    }

    /// <summary>
    /// 单字段条件的转换器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class FieldQueryFilterJSONConvert<TQueryModel> : IQueryFilterJSONConvert<TQueryModel>
    {
        public bool TryConvertFromJSON(JObject v, out IQueryFilter<TQueryModel> queryFilter)
        {
            if (v.TryGetValue(nameof(FieldQueryFilter<TQueryModel>.Name), StringComparison.OrdinalIgnoreCase, out _) &&
                v.TryGetValue(nameof(FieldQueryFilter<TQueryModel>.Value), StringComparison.OrdinalIgnoreCase, out _) &&
                v.TryGetValue(nameof(FieldQueryFilter<TQueryModel>.Compare), StringComparison.OrdinalIgnoreCase, out _))
            {
                queryFilter = v.ToObject<FieldQueryFilter<TQueryModel>>();
                return true;
            }

            queryFilter = null;
            return false;
        }
    }
}
