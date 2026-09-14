using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Text.RegularExpressions;
using Serilog;

namespace shoe_shop_backend.Extensions.Logs
{
    public class CustomLogPrettySqlInterceptor : DbCommandInterceptor
    {
        private static readonly string[] Keywords =
        {
        "SELECT", "FROM", "WHERE", "ORDER BY", "GROUP BY", "HAVING",
        "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "VALUES", "SET", "INSERT INTO", "UPDATE"
    };

        private static string Prettify(string sql)
        {
            foreach (var kw in Keywords)
                sql = Regex.Replace(sql, $@"(?<!^)\b{Regex.Escape(kw)}\b", $"\n{kw}", RegexOptions.IgnoreCase);
            return sql.Trim();
        }

        private static void LogCommand(DbCommand command, string phase)
        {
            var sql = Prettify(command.CommandText);
            var parameters = string.Join(", ",
                command.Parameters.Cast<DbParameter>()
                    .Select(p => $"{p.ParameterName}='{p.Value}'"));

            Log.ForContext("SourceContext", "PrettySql")
               .Information("[{Phase}]\n{Sql}\nParams: {Params}", phase, sql, parameters);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            LogCommand(command, "SELECT");
            return base.ReaderExecuting(command, eventData, result);
        }

        public override InterceptionResult<int> NonQueryExecuting(
            DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            LogCommand(command, "EXEC");
            return base.NonQueryExecuting(command, eventData, result);
        }
    }
}
