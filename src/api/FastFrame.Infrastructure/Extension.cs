﻿using AspectCore.Extensions.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure
{
    public static class Extension
    {
        public static async Task<PageList<T>> PageListAsync<T>(this IQueryable<T> query, PagePara pageInfo)
        {
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
        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, string kw, IEnumerable<Filter> filters)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (kw.IsNullOrWhiteSpace() && (filters == null || !filters.Any()))
                return query;

            foreach (var item in filters)
            {
                if (item.Value?.Trim().ToLower() == "null")
                    item.Value = null;
            }

            if (!kw.IsNullOrWhiteSpace())
            {
                var props = typeof(T).GetProperties()
                    .Where(x => x.PropertyType == typeof(string) && !x.Name.EndsWith("Id"));
                query = query.Where(string.Join(" or ", props.Select(x => $"{x.Name}.Contains(@0)")), kw);
            }

            foreach (var item in filters)
            {
                var conds = item.Name.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries);

                if (!conds.Any())
                    continue;

                if (item.Compare.ToLower() == "$")
                {
                    var values = item.Value.Split("".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (values.Any())
                    {
                        var queryStr = string.Join(" or ", conds.SelectMany((r) => values.Select((x, i) => $"@{r}.Contains(@{i})")));
                        query = query.Where(queryStr, values);
                    }
                    else
                    {
                        var queryStr = string.Join(" or ", conds.Select((r) => $"{r}.Contains(@0)"));
                        query = query.Where(queryStr, item.Value);
                    }
                }
                else if (item.Compare.ToLower().EndsWith("in"))
                {
                    var names = item.Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    var parameterExpression = Expression.Parameter(typeof(T), "v");
                    var propertyInfo = typeof(T).GetProperty(names[0]);
                    var memberExpression = Expression.Property(parameterExpression, propertyInfo);

                    foreach (var name in names.Skip(1))
                    {
                        propertyInfo = propertyInfo.PropertyType.GetProperty(name);
                        memberExpression = Expression.Property(parameterExpression, propertyInfo);
                    }

                    var values = item.Value.Split("".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (!values.Any())
                    {
                        continue;
                    }
                    if (!propertyInfo.PropertyType.IsArray)
                    {
                        //var queryStr = string.Join(" or ", values.Select((value, index) => $"{item.Name} == @{index}"));
                        query = query.Where($"@0.Contains({item.Name})", values);
                    }
                    else
                    {
                        var queryStr = string.Join(" or ", values.Select((value, index) => $"{item.Name}.Contains(@{index})"));
                        query = query.Where(queryStr, values);
                    }
                }
                else
                {
                    var queryStr = string.Join(" or ", conds.Select((r) => $"{r} {item.Compare} @0"));
                    query = query.Where(queryStr, item.Value);
                }

            }

            return query;
        }

        public static void WriteCodeLine(this StreamWriter writer, string line, int tagCount = 0)
        {
            writer.WriteLine($"{new string('\t', tagCount)}{line}");
        }

        public static string ToFirstLower(this string @this)
        {
            var arr = @this.ToArray();
            arr[0] = char.ToLower(arr[0]);
            return new string(arr);
        }

        /// <summary>
        /// 判空
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string @in)
        {
            return string.IsNullOrWhiteSpace(@in);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="in"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] ToSplitArray(this string @in, string separator = ",;")
        {
            if (@in.IsNullOrWhiteSpace())
                return Array.Empty<string>();

            return @in.Split(separator.ToArray());
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="in"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToJoinString(this IEnumerator<string> @in, string separator = ",")
        {
            if (@in is null)
            {
                return string.Empty;
            }

            return string.Join(separator, @in);
        }

        /// <summary>
        /// ToBase64
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static string ToBase64(this string @in)
            => @in.IsNullOrWhiteSpace() ? string.Empty : Convert.ToBase64String(Encoding.Default.GetBytes(@in));

        /// <summary>
        /// ToMD5
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static string ToMD5(this string @in)
        {
            if (@in == null)
                return null;

            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.Default.GetBytes(@in));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }

        public static string ToMD5(this Stream @in)
        {
            if (@in == null)
                return null;
            using (var md5 = MD5.Create())
            {
                @in.Position = 0;
                var result = md5.ComputeHash(@in);
                @in.Position = 0;
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="in"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetValue(this object @in, string propName)
        {
            return @in.GetType().GetProperty(propName).GetReflector().GetValue(@in);
        }

        /// <summary>
        /// 属性映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IQueryable<TTarget> MapTo<TSource, TTarget>(this IQueryable<TSource> sources)
        {
            var expression = ExpressionExtended<TSource, TTarget>.GetMapToExpression();
            return sources.Select(expression);
        }

        /// <summary>
        /// 属性映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource sources)
        {
            var func = ExpressionExtended<TSource, TTarget>.GetMapToDelegate();
            return func(sources);
        }

        /// <summary>
        /// 属性映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void MapSet<TSource, TTarget>(this TSource source, TTarget target)
        {
            var @delegate = ExpressionExtended<TSource, TTarget>.GetMapSetDelegate();
            @delegate(source, target);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static string ToJson(this object @in) =>
            @in == null ? "{}" : JsonConvert.SerializeObject(@in, new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd HH:mm",
                Converters = new[] {
                    new StringEnumConverter()
                }
            });

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static JObject ToJObject(this string @in)
            => @in.IsNullOrWhiteSpace() ? new JObject() : (JObject)JsonConvert.DeserializeObject(@in);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="in"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string @in)
        {
            if (@in.IsNullOrWhiteSpace())
            {
                return default;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(@in);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
