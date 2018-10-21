using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FastFrame.Infrastructure
{
    public static class Extension
    {
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

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="in"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetValue(this object @in, string propName)
        {
            return @in.GetType().GetProperty(propName).GetValue(@in, null);
        }
    }
}
