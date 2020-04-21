using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase
{
    public class SqlBuilder
    {
        private string LeftEscapeChar { get; set; }
        private string RightEscapeChar { get; set; }
        private string ParmChar { get; set; }

        public SqlBuilder(string leftEscapeChar, string rightEscapeChar, string parmChar)
        {
            LeftEscapeChar = leftEscapeChar;
            RightEscapeChar = rightEscapeChar;
            ParmChar = parmChar;
        }

        #region EscapeChar 关键字

        public string UseEscapeChar(PropertyInfo prop)
        {
            return UseEscapeChar(prop.Name);
        }

        public string UseEscapeChar(string propStr)
        {
            return string.Format("{0}{1}{2}", LeftEscapeChar, propStr, RightEscapeChar);
        }

        public List<string> UseEscapeChar(ICollection<string> props)
        {
            var result = new List<string>();
            foreach (var item in props)
            {
                result.Add(UseEscapeChar(item));
            }
            return result;
        }

        public List<string> UseEscapeChar(ICollection<PropertyInfo> props)
        {
            var result = new List<string>();
            foreach (var item in props)
            {
                result.Add(UseEscapeChar(item));
            }
            return result;
        }

        #endregion EscapeChar 关键字

        #region ParmChar 参数关键字

        public string UseParmChar(string parm)
        {
            return string.Format("{0}{1}", ParmChar, parm);
        }

        public string UseParmChar(PropertyInfo parm)
        {
            return UseParmChar(parm.Name);
        }

        public List<string> UseParmChar(ICollection<PropertyInfo> parms)
        {
            var result = new List<string>();
            foreach (var item in parms)
            {
                result.Add(UseParmChar(item.Name));
            }
            return result;
        }

        public List<string> UseParmChar(ICollection<string> parms)
        {
            var result = new List<string>();
            foreach (var item in parms)
            {
                result.Add(UseParmChar(item));
            }
            return result;
        }

        #endregion ParmChar 参数关键字

        public List<string> GetEqualConditions(ICollection<PropertyInfo> props)
        {
            var result = new List<string>();
            foreach (var item in props)
            {
                result.Add(string.Format("{0} = {1}", UseEscapeChar(item), UseParmChar(item)));
            }
            return result;
        }

        public List<string> GetEqualConditions(params string[] props)
        {
            var result = new List<string>();
            foreach (var item in props)
            {
                result.Add(string.Format("{0} = {1}", UseEscapeChar(item), UseParmChar(item)));
            }
            return result;
        }

        #region SQL拼接

        public StringBuilder BuildSelectSqlNoWhere<T>(string tableName, PropertyInfo[] selectPops)
        {
            StringBuilder sb = new StringBuilder();
            var props = UseEscapeChar(selectPops);
            sb.AppendFormat("SELECT {0} FROM {1} ", string.Join(",", props), tableName);
            return sb;
        }

        public StringBuilder BuildSelectSql<T>(string tableName, PropertyInfo[] selectPops)
        {
            StringBuilder sb = new StringBuilder();
            var props = UseEscapeChar(selectPops);
            sb.AppendFormat("SELECT {0} FROM {1} WHERE 1 = 1 ", string.Join(",", props), tableName);
            return sb;
        }

        public StringBuilder BuildSelectCountSqlNoWhere(string tableName)
        {
            return new StringBuilder("SELECT Count(*) FROM " + tableName + " ");
        }

        public StringBuilder BuildSelectCountSql(string tableName)
        {
            return new StringBuilder("SELECT Count(*) FROM " + tableName + " WHERE 1 = 1");
        }

        // ********** ********** ********** **********

        public StringBuilder BuildInsertSql<T>(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            var properties = typeof(T).GetProperties().ToList();
            var props = UseEscapeChar(properties);
            var parms = UseParmChar(properties);
            sb.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2}) ", tableName, string.Join(",", props), string.Join(",", parms));
            return sb;
        }

        // ********** ********** ********** **********

        public StringBuilder BuildUpdateByIdSql<T>(string tableName, params string[] updateFields)
        {
            StringBuilder sb = new StringBuilder();
            var setValues = GetEqualConditions(updateFields);
            var whereProp = GetEqualConditions(SchemaManage.GetPrimaryKey<T>().Name);
            sb.AppendFormat("UPDATE {0} SET {1} WHERE {2} ", tableName, string.Join(",", setValues), string.Join(" AND ", whereProp));
            return sb;
        }

        // ********** ********** ********** **********

        public StringBuilder BuildDeleteTableSqlNoWhere(string tableName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            return sb;
        }

        public StringBuilder BuildDeleteTableSql(string tableName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} WHERE 1 = 1 ", tableName);
            return sb;
        }

        public StringBuilder BuildDeleteByIdSql<T>(string tableName)
        {
            var sb = BuildDeleteTableSql(tableName);
            sb.AppendFormat(" AND {0} ", GetEqualConditions(SchemaManage.GetPrimaryKey<T>().Name).First());
            return sb;
        }

        #endregion SQL拼接

        /// <summary>
        /// 对象转换DbParameter
        /// </summary>
        /// <returns></returns>
        public DbParameter GetParameter<T>(DbCommand cmd, T entity, PropertyInfo propertyInfo)
        {
            var result = cmd.CreateParameter();
            var value = propertyInfo.GetValue(entity, null);

            result.ParameterName = ParmChar + propertyInfo.Name;
            result.DbType = GetDbType(value);
            result.Value = value;
            return result;
        }

        public DbType GetDbType<T>(T value)
        {
            var result = new DbType();

            if (value != null)
            {
                if (value.GetType() == typeof(Int32) || value.GetType() == typeof(Int32?))
                { result = DbType.Int32; }

                if (value.GetType() == typeof(Decimal) || value.GetType() == typeof(Decimal?))
                { result = DbType.Decimal; }

                if (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTime?))
                { result = DbType.DateTime; }

                if (value.GetType() == typeof(bool))
                { result = DbType.Boolean; }

                if (value.GetType() == typeof(string))
                { result = DbType.String; }
            }

            return result;
        }
    }
}