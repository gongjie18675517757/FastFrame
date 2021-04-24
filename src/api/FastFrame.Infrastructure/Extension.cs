using AspectCore.Extensions.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure
{
    public static class Extension
    {
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                return default;
            }

            var resultList = new List<T>();
            await foreach (var item in enumerable)
            {
                resultList.Add(item);
            }

            return resultList;
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

        public static void WriteCodeLine(this StreamWriter writer, string line, int tagCount = 0)
        {
            writer.WriteLine($"{new string('\t', tagCount)}{line}");
        }

        public static void WriteCodeLine(this StringBuilder writer, string line, int tagCount = 0)
        {
            writer.AppendLine($"{new string('\t', tagCount)}{line}");
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

        public static string ToHexString(this string @in, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(@in))
                return default;

            if (encoding == null)
                encoding = Encoding.UTF8;

            return BitConverter.ToString(encoding.GetBytes(@in)).Replace("-", "").ToLower();
        }

        public static string FromHexString(this string @in, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(@in))
                return default;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = new byte[@in.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                var s = new string(new char[] { @in[i * 2], @in[i * 2 + 1] });
                var b = byte.Parse(s, System.Globalization.NumberStyles.HexNumber);

                bytes[i] = b;
            }

            return encoding.GetString(bytes);
        }


        /// <summary>
        /// ToBase64
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static string ToBase64(this string @in)
            => @in.IsNullOrWhiteSpace() ? string.Empty : Convert.ToBase64String(Encoding.Default.GetBytes(@in));

        private static readonly char[] base64CodeArray = new char[]
           {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '0', '1', '2', '3', '4',  '5', '6', '7', '8', '9', '+', '/', '='
           };

        /// <summary>
        /// FromBase64
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        public static string FromBase64(this string @in)
        {
            try
            {
                if (@in.Length % 4 != 0)
                    return @in;
                if (@in.Any(c => !base64CodeArray.Contains(c)))
                    return @in;
                return @in.IsNullOrWhiteSpace() ? string.Empty : Encoding.Default.GetString(Convert.FromBase64String(@in));
            }
            catch (Exception)
            {
                return default;
            }
        }

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
            return @in?.GetType().GetProperty(propName)?.GetReflector().GetValue(@in);
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
        /// <param name="haveBase64Dencode"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string @in, bool haveBase64Dencode = false)
        {
            if (@in.IsNullOrWhiteSpace())
            {
                return default;
            }
            try
            {
                if (haveBase64Dencode)
                    @in = @in.FromBase64();
                return JsonConvert.DeserializeObject<T>(@in);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
