using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FastFrame.Database
{
    /// <summary>
    /// 格式化EF生成的语句
    /// </summary>
    public class FmtCommandInterceptor : DbCommandInterceptor, IDbCommandInterceptor
    {
        /*替换掉 mysql 中：LOCATE(CONVERT USING utf8mb4) 中的语句*/
        public static readonly Regex Regex_Replace_MySql_Like
                = new Regex(@"LOCATE\(CONVERT\('(?<v>.+?)' USING utf8mb4\) COLLATE utf8mb4_bin, (?<k>`.+`?)\) > 0", 
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.CommandText = Regex_Replace_MySql_Like.Replace(command.CommandText, " ${k} like '%${v}%' ");
            return base.ReaderExecuting(command, eventData, result);
        }

        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            /*优化ＥＦ生成的查询语句*/

            /*1,处理未正确的在MYSQL下生成LIKE语句*/
            /*场景1：LOCATE(CONVERT('test' USING utf8mb4) COLLATE utf8mb4_bin, `b`.`account`) > 0*/
            command.CommandText = Regex_Replace_MySql_Like.Replace(command.CommandText, " ${k} like '%${v}%' ");　

            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }
    }
}
