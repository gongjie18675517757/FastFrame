using AspectCore.Extensions.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace FastFrame.Infrastructure
{
    public static class Extension
    {
        /// <summary>
        /// 递归子节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="childSelector"></param>
        /// <param name="super"></param>
        /// <param name="eachAction"></param>
        public static void EachLoopChild<T>(this IEnumerable<T> enumerable, Func<T, IEnumerable<T>> childSelector, T super, Action<(T patent, T child)> eachAction)
        {
            if (enumerable == null)
                return;

            if (childSelector == null)
                throw new ArgumentException(null, nameof(childSelector));

            if (eachAction == null)
                throw new ArgumentException(null, nameof(eachAction));

            foreach (var item in enumerable)
            {
                eachAction((super, item));
                EachLoopChild(childSelector(item), childSelector, item, eachAction);
            }
        }

        /// <summary>
        /// 展开子节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="childSelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectLoopChild<T>(this IEnumerable<T> enumerable, Func<T, IEnumerable<T>> childSelector)
        {
            if (enumerable == null)
                yield break;

            if (childSelector == null)
                throw new ArgumentException(null, nameof(childSelector));

            foreach (var item in enumerable)
            {
                yield return item;

                var children = SelectLoopChild(childSelector(item), childSelector);

                if (children != null && children.Any())
                    foreach (var child in children)
                        yield return child;
            }
        }

        /// <summary>
        /// 组合树
        /// </summary>
        /// <typeparam name="T">树类型</typeparam>
        /// <typeparam name="TKey">树键类型</typeparam>
        /// <param name="enumerable">未组合的一维节点</param>
        /// <param name="patentFunc">取上级键</param>
        /// <param name="keyFunc">取当前健</param>
        /// <param name="setChildAction">设置下级</param>
        /// <param name="parent_id">指定当前父节点</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectLoopChild<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> patentFunc, Func<T, TKey> keyFunc, Action<T, IEnumerable<T>> setChildAction, TKey parent_id)
        {
            if (enumerable is null)
                yield break;
            foreach (var item in enumerable)
            {
                if (EqualityComparer<TKey>.Default.Equals(patentFunc(item), parent_id))
                {
                    var children = SelectLoopChild(enumerable, patentFunc, keyFunc, setChildAction, keyFunc(item));
                    setChildAction(item, children);
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 获取字典值，如果没有，就返回默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TVal TryGetValueOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dic, TKey key)
        {
            if (dic.TryGetValue(key, out var val))
                return val;

            return default;
        }

        /// <summary>
        /// 获取字典值，如果没有，就根据指定委托创建一个
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TVal TryGetValueOrCreate<TKey, TVal>(this IDictionary<TKey, TVal> dic, TKey key, Func<TVal> func)
        {
            if (dic.TryGetValue(key, out var val))
                return val;

            val = func();
            dic.Add(key, val);
            return val;
        }


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

            if (!System.Text.RegularExpressions.Regex.IsMatch(@in, "^[0-9a-f]+$"))
                return @in;

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

            using var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(@in));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "").ToLower();
        }

        public static string ToMD5(this Stream @in)
        {
            if (@in == null)
                return null;
            using var md5 = MD5.Create();
            @in.Position = 0;
            var result = md5.ComputeHash(@in);
            @in.Position = 0;
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "").ToLower();
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
        /// <param name="haveHexDencode"></param>
        /// <param name="converters"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string @in, bool haveHexDencode = false, params JsonConverter[] converters)
        {
            if (@in.IsNullOrWhiteSpace())
            {
                return default;
            }
            try
            {
                if (haveHexDencode)
                    @in = @in.FromHexString();

                return JsonConvert.DeserializeObject<T>(@in, converters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public static bool TryToObject(this string @in, Type type, out object result)
        {
            result = null;

            if (@in.IsNullOrWhiteSpace())
                return false;

            try
            {
                result = JsonConvert.DeserializeObject(@in, type);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
