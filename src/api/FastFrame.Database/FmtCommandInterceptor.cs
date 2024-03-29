﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FastFrame.Database
{
    /// <summary>
    /// 格式化EF生成的语句
    /// </summary>
    public partial class FmtCommandInterceptor : DbCommandInterceptor, IDbCommandInterceptor
    {
        /*替换掉 mysql 中：LOCATE(CONVERT USING utf8mb4) 中的语句*/
        public static readonly (Regex regex, string replace)[] regexArr;

        static FmtCommandInterceptor()
        {
            regexArr
                = new (Regex, string)[] {
                    (MyRegex1()," ${k} like '%${v}%' "),
                    (MyRegex()," ${k} like CONCAT('%',@${v},'%') "),
                };
        }
    
       
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            /*优化ＥＦ生成的查询语句*/
            command.CommandText = FormatterSql(command.CommandText);
            return base.ReaderExecuting(command, eventData, result);
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            /*优化ＥＦ生成的查询语句*/
            command.CommandText = FormatterSql(command.CommandText);

            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        private static string FormatterSql(string sql)
        {
            foreach (var (regex, replace) in regexArr)
            {
                if (regex.IsMatch(sql))
                    sql = regex.Replace(sql, replace);
            }

            return sql;
        }

        [GeneratedRegex("LOCATE\\(CONVERT\\(@(?<v>.+?) USING utf8mb4\\) COLLATE utf8mb4_bin, (?<k>`.+`?)\\) > 0", RegexOptions.IgnoreCase | RegexOptions.Compiled, "zh-CN")]
        private static partial Regex MyRegex();


        [GeneratedRegex("LOCATE\\(CONVERT\\('(?<v>.+?)' USING utf8mb4\\) COLLATE utf8mb4_bin, (?<k>`.+`?)\\) > 0", RegexOptions.IgnoreCase | RegexOptions.Compiled, "zh-CN")]
        private static partial Regex MyRegex1();
    }
}
