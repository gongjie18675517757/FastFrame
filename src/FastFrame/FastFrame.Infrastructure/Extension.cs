using AspectCore.Extensions.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;

namespace FastFrame.Infrastructure
{
    public static class Extension
    {
        public static IQueryable<T> DynamicSort<T>(this IQueryable<T> query, SortInfo sortInfo)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (sortInfo == null)
            {
                sortInfo = new SortInfo()
                {
                    Name = "Id",
                    Mode = "asc"
                };
            }

            return query.OrderBy($"{sortInfo.Name} {sortInfo.Mode}");
        }
        public static IQueryable<T> DynamicQuery<T>(this IQueryable<T> query, QueryCondition condition)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (condition == null)
            {
                return query;
            }
            foreach (var item in condition.Filters)
            {
                if (item.Value?.Trim().ToLower() == "null")
                    item.Value = null;
            }

            if (!condition.KeyWord.IsNullOrWhiteSpace())
            {
                var props = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(string) && !x.Name.EndsWith("Id"));
                query = query.Where(string.Join(" or ", props.Select(x => $"{x.Name}.Contains(@0)")), condition.KeyWord);
            }

            foreach (var item in condition.Filters)
            {
                var conds = item.Name.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                var values = item.Value.Split("".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                if (!conds.Any() && !values.Any())
                    continue;

                if (item.Compare.ToLower() == "$")
                {
                    var queryStr = string.Join(" or ", conds.SelectMany((r, i) => $"{r}.Contains(@{i})"));
                    query = query.Where(queryStr, values);
                }
                else
                {
                    var queryStr = string.Join(" or ", conds.SelectMany((r, i) => $"{r} {item.Compare} @{i}"));
                    query = query.Where($"{item.Name} {item.Compare} @0", item.Value);
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
                return default(T);
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(@in);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
