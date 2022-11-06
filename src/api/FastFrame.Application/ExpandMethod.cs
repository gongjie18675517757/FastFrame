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

namespace FastFrame.Application
{
    public static class FilterExtensions<T>
    {
        /// <summary>
        /// 默认过滤表达式
        /// </summary>
        public static Expression<Func<T, bool>> DefaultPredicate => v => true;

        /// <summary>
        /// 返回多级属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propNamePath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 生成条件表达式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="compare"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> MakePredicate(string name, string compare, string value)
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

                var propertyInfo = GetPropertyInfoByNames(typeof(T), name);
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

            return DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, queryStr, values: values);
        }
    }

    /// <summary>
    /// 字面量查询项
    /// </summary>
    public class Filter : IFilter
    {
        /// <summary>
        /// 属性名称
        /// 支持多级 a.b.c
        /// 支持多个 a.b;a.c
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 比较方式  
        /// </summary>
        public string Compare { get; set; }

        /// <summary>
        /// 比较值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 生成表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Expression<Func<T, bool>> MakePredicate<T>()
        {
            return FilterExtensions<T>.MakePredicate(Name, Compare, Value);
        }
    }

    /// <summary>
    /// 表达式查询项
    /// </summary>
    public abstract class ExtensionFilter : IFilter
    {
        private readonly Expression expression;

        protected ExtensionFilter(Expression expression)
        {
            this.expression = expression;
        }

        public Expression<Func<T, bool>> MakePredicate<T>() => (Expression<Func<T, bool>>)expression;
    }

    /// <summary>
    /// 表达式查询项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtensionFilter<T> : ExtensionFilter
    {
        public ExtensionFilter(Expression<Func<T, bool>> expression) : base(expression)
        {
        }
    }

    public class FilterJsonConvert : JsonConverter<IFilter>
    {
        public static FilterJsonConvert Default { get; } = new FilterJsonConvert();

        public override IFilter ReadJson(JsonReader reader, Type objectType, IFilter existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var obj = JObject.Load(reader);

            using (var sr = obj.CreateReader())
            {
                existingValue = new Filter();
                serializer.Populate(sr, existingValue);
            }

            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, IFilter value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    /// <summary>
    /// 查询列表参数
    /// </summary>
    public class Pagination : IPagination
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
        public virtual IEnumerable<KeyValuePair<FilterMode, IEnumerable<IFilter[]>>> Filters { get; set; }
            = Array.Empty<KeyValuePair<FilterMode, IEnumerable<IFilter[]>>>();

        /// <summary>
        /// 排序列名称
        /// </summary>
        public string SortName { get; set; } = "Id";

        /// <summary>
        /// 排序方式
        /// </summary>
        public string SortMode { get; set; } = "Desc";

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Pagination FromJson(string json)
        {
            return json.ToObject<Pagination>(true, FilterJsonConvert.Default);
        }

        //public static bool TryParse(string qs, IFormatProvider provider, out Pagination pagination)
        //{
        //    pagination = FromJson(qs);
        //    return pagination != null;
        //}
    }

    public class PageList<T> : IPageList<T>
    {
        public int Total { get; set; }

        public IReadOnlyList<T> Data { get; set; } = new List<T>();
    }

    public static class ExpandMethod
    {
        public static async Task<IPageList<T>> PageListAsync<T>(this IQueryable<T> query, IPagination pageInfo)
        {
            pageInfo ??= new Pagination();
            query = query.DynamicQuery(pageInfo.KeyWord, pageInfo.Filters.ToArray());

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

        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, params IFilter[] filters)
        {
            foreach (var f in filters)
            {
                var expression = f.MakePredicate<T>();
                query = query.Where(expression);
            }

            return query;
        }

        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, string kw, params KeyValuePair<FilterMode, IEnumerable<IFilter[]>>[] kvsFilter)
        {
            foreach (var kv in kvsFilter)
            {
                Expression<Func<T, bool>> mainExpression = null;

                foreach (var brr in kv.Value)
                {
                    var expression = PredicateExtensions.True<T>();

                    foreach (var f in brr)
                    {
                        var subExpression = f.MakePredicate<T>();
                        expression = expression.And(subExpression);
                    }

                    if (mainExpression == null)
                    {
                        mainExpression = expression;
                    }
                    else
                    {
                        switch (kv.Key)
                        {
                            case FilterMode.and:
                                mainExpression = mainExpression.And(expression);
                                break;
                            case FilterMode.or:
                                mainExpression = mainExpression.Or(expression);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (mainExpression != null)
                    query = query.Where(mainExpression);
            }



            if (!kw.IsNullOrWhiteSpace())
            {
                var props = typeof(T).GetProperties()
                    .Where(x => x.PropertyType == typeof(string) && !x.Name.EndsWith("Id"));
                query = query.Where(string.Join(" or ", props.Select(x => $"{x.Name}.Contains(@0)")), kw);
            }

            return query;
        }

        //public static PropertyInfo GetPropertyInfoByNames(Type type, string propNamePath)
        //{
        //    var names = propNamePath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        //    var propertyInfo = type.GetProperty(names[0]);
        //    if (propertyInfo == null)
        //        return null;

        //    foreach (var name in names.Skip(1))
        //    {
        //        propertyInfo = propertyInfo.PropertyType.GetProperty(name);
        //        if (propertyInfo == null)
        //            return null;
        //    }

        //    return propertyInfo;
        //}

        //private static (string key, List<string> strList, List<object> valList) CalcFilter<T>(ref int rIndex, KeyValuePair<string, List<Filter>> kvs)
        //{
        //    var filters = kvs.Value;
        //    var queryStrList = new List<string>();
        //    var queryValList = new List<object>();
        //    foreach (var item in filters)
        //    {
        //        var conds = item.Name.Split(";,".ToArray(), StringSplitOptions.RemoveEmptyEntries);

        //        if (!conds.Any())
        //            continue;

        //        if (item.Compare.ToLower() == "$")
        //        {
        //            var values = item.Value.ToSplitArray(" ");
        //            if (values.Length == 0)
        //                continue;


        //            var qList = new List<string>();

        //            foreach (var value in values)
        //            {
        //                foreach (var cond in conds)
        //                {
        //                    qList.Add($"{cond}.Contains(@{rIndex})");
        //                }
        //                rIndex++;
        //            }
        //            var queryStr = string.Join(" or ", qList);
        //            queryStrList.Add(queryStr);
        //            queryValList.AddRange(values);
        //        }
        //        else if (item.Compare.ToLower().EndsWith("in"))
        //        {
        //            var values = item.Value.ToSplitArray(",;");
        //            if (values.Length == 0)
        //                continue;

        //            var propertyInfo = GetPropertyInfoByNames(typeof(T), item.Name);
        //            var operate = item.Compare.StartsWith("!") ? "!=" : "==";

        //            /*如果属性为数组，则要拆出来成多个OR*/
        //            if (propertyInfo.PropertyType.IsArray)
        //            {
        //                var qList = new List<string>();

        //                foreach (var v in values)
        //                {
        //                    qList.Add($"{item.Name}.Contains(@{rIndex++})");
        //                }
        //                var queryStr = string.Join(" or ", qList);
        //                queryStrList.Add(queryStr);
        //                queryValList.AddRange(values);
        //            }
        //            else
        //            {
        //                queryStrList.Add($"{operate}@{rIndex++}.Contains({item.Name})");
        //                queryValList.Add(values);
        //            }
        //        }
        //        else
        //        {
        //            var index = rIndex;
        //            var queryStr = string.Join(" or ", conds.Select((r) => $"{r} {item.Compare} @{index}"));
        //            queryStrList.Add(queryStr);
        //            queryValList.Add(item.Value);
        //            rIndex++;
        //        }
        //    }

        //    return (kvs.Key, queryStrList, queryValList);
        //}
    }

    public static class PredicateExtensions
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);
        }
    }



}
