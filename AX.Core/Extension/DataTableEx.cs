using AX.Core.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AX.Core.Extension
{
    public static class DataTableEx
    {
        #region 帮助方法

        /// <summary>
        /// 添加行数据
        /// 如果没有列 自动添加
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="colNames">列名</param>
        public static void AddColValue(this DataRow row, string colName, string value)
        {
            if (row.Table.Columns.Contains(colName))
            {
                row[colName] = value;
            }
            else
            {
                row.Table.Columns.Add(colName);
                row[colName] = value;
            }
        }

        /// <summary>
        /// 获取行中某列的 字符串 值
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">行对象</param>
        /// <param name="col">列对象</param>
        /// <returns>内容值</returns>
        public static string GetRowColStringValue(this DataTable table, DataRow row, DataColumn col)
        {
            if (col == null)
            {
                return string.Empty;
            }
            return string.Format("{0}", row[col]);
        }

        /// <summary>
        /// 获取行中某列的 decimal? 值
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">行对象</param>
        /// <param name="col">列对象</param>
        /// <returns>内容值</returns>
        public static Decimal? GetRowColDecimalValue(this DataTable table, DataRow row, DataColumn col)
        {
            var valueStr = table.GetRowColStringValue(row, col);
            if (string.IsNullOrWhiteSpace(valueStr))
            {
                return null;
            }
            Decimal value = 0;
            if (!Decimal.TryParse(valueStr, out value))
            {
                return null;
            }
            return value;
        }

        /// <summary>
        /// 获取行中的 DateTime? 值
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">行对象</param>
        /// <param name="col">列对象</param>
        /// <returns>内容值</returns>
        public static DateTime? GetRowColOADateTimeValue(this DataTable table, DataRow row, DataColumn col)
        {
            var valueStr = table.GetRowColStringValue(row, col);
            if (string.IsNullOrWhiteSpace(valueStr))
            {
                return null;
            }
            Double dateDouble;
            DateTime dateTime;

            if (double.TryParse(valueStr, out dateDouble))
            {
                return DateTime.FromOADate(dateDouble);
            }
            else
            {
                if (DateTime.TryParse(valueStr, out dateTime))
                {
                    return dateTime;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取行中某列的 int? 值
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">行对象</param>
        /// <param name="col">列对象</param>
        /// <returns>内容值</returns>
        public static int? GetRowColIntValue(this DataTable table, DataRow row, DataColumn col)
        {
            var valueStr = table.GetRowColStringValue(row, col);
            if (string.IsNullOrWhiteSpace(valueStr))
            {
                return null;
            }
            int value = 0;
            if (!int.TryParse(valueStr, out value))
            {
                return null;
            }
            return value;
        }

        /// <summary>
        ///  检查表格列 若不存在抛出异常 缺少列【{0}】
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="colNames">列名</param>
        public static void CheckRequiredCols(this DataTable table, params string[] colNames)
        {
            foreach (var item in colNames)
            {
                if (!table.Columns.Contains(item))
                {
                    throw new AXCoreException(string.Format("缺少列【{0}】", item));
                }
            }
        }

        #endregion 帮助方法

        /// <summary>
        /// 转换为 List
        /// </summary>
        public static List<T> ToList<T>(this DataTable table)
        {
            var result = new List<T>();
            var currentType = typeof(T);

            if (table == null || table.Rows.Count <= 0)
            { return result; }

            var propertyInfos = currentType.GetProperties();

            foreach (DataRow row in table.Rows)
            {
                T item = (T)Activator.CreateInstance(currentType);

                foreach (var prop in propertyInfos)
                {
                    //如果数据行没有该字段则忽略
                    if (row[prop.Name] == null)
                    { continue; }

                    object value = null;
                    if (prop.PropertyType.ToString().Contains("System.Nullable"))
                    {
                        value = Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(prop.PropertyType));
                    }
                    else
                    {
                        value = Convert.ChangeType(row[prop.Name], prop.PropertyType);
                    }
                    prop.SetValue(item, value, null);
                }

                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 转换为标准的CSV字符串
        /// </summary>
        public static String ToCsv(this DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}