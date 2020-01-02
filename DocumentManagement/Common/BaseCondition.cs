using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Common
{
    public class BaseCondition<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int FromRecord => PageSize * (PageIndex - 1);
        public List<FilterItem> FilterRuleList { get; set; } = new List<FilterItem>();
        public string IN_WHERE
        {
            get
            {
                if (FilterRuleList.Count == 0 || FilterRuleList == null)
                {
                    return null;
                }
                else
                {
                    var result = new StringBuilder();
                    foreach (var item in FilterRuleList)
                    {
                        var valueKillSqlInjection = KillSqlInjection(item.value);
                        if (!string.IsNullOrEmpty(valueKillSqlInjection))
                        {
                            /*
                             * Type: single string value
                             * Condition type: AND
                             */
                            if (item.op == "and_contains")
                            {
                                //result.Append(" AND LOWER(" + item.field + ") LIKE N'%" + valueKillSqlInjection.ToLower().Replace(" ", "%") + "%'");
                                result.Append(" AND LOWER(" + item.field + ") LIKE N'%" + valueKillSqlInjection.ToLower().Trim() + "%'");
                            }

                            /*
                             * Type: single string value
                             * Condition type: AND
                             */
                            if (item.op == "or_contains")
                            {
                                result.Append(" OR LOWER(" + item.field + ") LIKE N'%" + valueKillSqlInjection.ToLower().Trim() + "%'");
                            }

                            /*
                             * Type: single string value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_string")
                            {
                                result.Append(" And " + item.field + " IN (N'" + valueKillSqlInjection + "')");
                            }

                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_int")
                            {
                                result.Append(" And " + item.field + " IN (" + valueKillSqlInjection + ")");
                            }

                            /*
                             * Type: list string value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_strings")
                            {
                                if (valueKillSqlInjection.IndexOf(",") > 0)
                                {
                                    valueKillSqlInjection = valueKillSqlInjection.ToLower().Replace(",", "',N'");
                                }

                                result.Append(" And " + item.field + " IN (N'" + valueKillSqlInjection + "')");
                            }

                            /*
                             * Type: list int value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_ints")
                            {
                                if (valueKillSqlInjection.IndexOf(",") > 0)
                                {
                                    valueKillSqlInjection = valueKillSqlInjection.ToLower();
                                }

                                result.Append(" And " + item.field + " IN (" + valueKillSqlInjection + ")");
                            }
                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "=")
                            {
                                result.Append(" AND " + item.field + " = " + valueKillSqlInjection + "");
                            }

                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "less")
                            {
                                result.Append(" AND " + item.field + " < " + valueKillSqlInjection);
                            }

                            /*
                             * Type: single value
                             * Condition type: AND
                             */
                            else if (item.op == "less_or_equal")
                            {
                                result.Append(" AND " + item.field + " <= " + "'" + valueKillSqlInjection + "'");
                            }

                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "greater")
                            {
                                result.Append(" AND " + item.field + " > " + valueKillSqlInjection);
                            }

                            /*
                             * Type: single value
                             * Condition type: AND
                             */
                            else if (item.op == "greater_or_equal")
                            {
                                result.Append(" AND " + item.field + " >= " + "'" + valueKillSqlInjection + "'");
                            }

                            /*
                             * Type: from 2 or more date and time compare
                             * Condition type: AND
                             */
                            else if (item.op == "between")
                            {

                                var arrVl = valueKillSqlInjection.Split(':');
                                var vl1 = arrVl[0];
                                var vl2 = arrVl[1];
                                if (!string.IsNullOrEmpty(vl1))
                                {
                                    result.Append(" AND " + item.field + " >= CONVERT (DATETIME, '" + vl1 + "', 103)");
                                }

                                if (!string.IsNullOrEmpty(vl2))
                                {
                                    result.Append(" AND " + item.field + " <= CONVERT(DATETIME, '" + vl2 + "', 103)");
                                }
                            }


                            /*
                            * Type: searching multiple dates,remove time from dates
                            * Condition type: AND
                            */
                            else if (item.op == "and_date_equal")
                            {
                                if (valueKillSqlInjection.Contains(","))
                                {
                                    string[] dateValues = new string[1];
                                    dateValues = valueKillSqlInjection.Split(',');
                                    string query = string.Empty;
                                    for (int i = 0; i < dateValues.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(dateValues[i]))
                                        {
                                            if (i == 0)
                                            {
                                                query = " AND ( CAST(" + item.field + " AS DATE) = CAST('" + dateValues[i] + "' AS DATE)";
                                            }
                                            else
                                            {
                                                query = query + " OR CAST(" + item.field + " AS DATE) = CAST('" + dateValues[i] + "' AS DATE)";
                                            }
                                        }
                                    }
                                    query = query + ")";
                                    result.Append(query);
                                }
                                else
                                {
                                    result.Append(" AND CAST(" + item.field + " AS DATE) = CAST('" + valueKillSqlInjection + "' AS DATE)");
                                }
                            }

                            /*
                           * Type: searching date between from date and to date
                           * Condition type: AND
                           */
                            else if (item.op == "and_date_between")
                            {
                                if (valueKillSqlInjection.Contains("-"))
                                {
                                    string[] dateValues = new string[1];
                                    dateValues = valueKillSqlInjection.Split('-');
                                    string query = string.Empty;
                                    for (int i = 0; i < dateValues.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(dateValues[i]))
                                        {
                                            if (i == 0)
                                            {
                                                query = " AND ( CAST(" + item.field + " AS DATE) >= CAST('" + DateTime.Parse(dateValues[i]).ToString("yyyy/MM/dd") + "' AS DATE)";
                                            }
                                            else
                                            {
                                                query = query + " AND CAST(" + item.field + " AS DATE) <= CAST('" + DateTime.Parse(dateValues[i]).ToString("yyyy/MM/dd") + "' AS DATE)";
                                            }
                                        }
                                    }
                                    query = query + ")";
                                    result.Append(query);
                                }
                                else
                                {
                                    result.Append(" AND CAST(" + item.field + " AS DATE) >= CAST('" + valueKillSqlInjection + "' AS DATE)");
                                }
                            }

                            else if (item.op == "not_equal_number")
                            {
                                int val;
                                bool isNumber = int.TryParse(item.value, out val);

                                if (isNumber)
                                {
                                    result.Append($" AND {item.field} <> {item.value}");
                                }
                            }

                            /*
                             * Group condition ex: AND (A.ABC = value OR A.BCD = value)
                             */
                            else if (item.op == "and_group_contains_with_or")
                            {
                                var fields = item.field.Split(',');
                                for (var i = 0; i < fields.Length; i++)
                                {
                                    if (i == 0)
                                        result.Append($" AND ( {fields[i]} LIKE N'%{valueKillSqlInjection}%' ");
                                    else if (i == (fields.Length - 1))
                                        result.Append($" OR {fields[i]} LIKE N'%{valueKillSqlInjection}%' )");
                                    else
                                        result.Append($" OR {fields[i]} LIKE N'%{valueKillSqlInjection}%' ");
                                }
                            }
                        }
                    }
                    return result.ToString();
                }
            }

        }
        public string IN_SORT { get; set; }
        T Item { get; set; }
        private string KillSqlInjection(string sqlString)
        {
            if (!string.IsNullOrEmpty(sqlString))
            {
                string[] chuoiDauRa = { "--", ";--", ";", "/*", "*/", "@@", "char", "nchar", "varchar", "nvarchar", "alter", "begin",
                                    "cast", "create", "cursor", "declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert",
                                    "kill", "select", "sys", "sysobjects", "syscolumns", "table", "update" };
                for (var i = 0; i <= chuoiDauRa.Length - 1; i++)
                {
                    sqlString = sqlString.ToLower().Replace(chuoiDauRa[i], "");
                }
            }
            return sqlString;
        }
    }
}
