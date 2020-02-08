using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common
{
    public static class Utilities
    {
        public static string KillSqlInjection(string sqlString)
        {
            if (!string.IsNullOrEmpty(sqlString))
            {
                string[] chuoiDauRa = { "--", ";--", ";", "/*", "*/", "@@", "'", "char", "nchar", "varchar", "nvarchar", "alter", "begin",
                                    "cast", "create", "cursor", "declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert",
                                    "kill", "select", "sys", "sysobjects", "syscolumns", "table", "update" };
                for (var i = 0; i <= chuoiDauRa.Length - 1; i++)
                {
                    if (sqlString.Contains(chuoiDauRa[i]))
                    {
                        sqlString = sqlString.ToLower().Replace(chuoiDauRa[i], "");
                    }
                }
            }
            return sqlString;
        }
    }
}
