﻿//using AX.Core.DataBase.Schema;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace AX.Core.DataBase
//{
//    public class SqlBuilder
//    {
//        private string LeftEscapeChar { get; set; }
//        private string RightEscapeChar { get; set; }
//        private string ParmChar { get; set; }

//        public SqlBuilder(string leftEscapeChar, string rightEscapeChar, string parmChar)
//        {
//            LeftEscapeChar = leftEscapeChar;
//            RightEscapeChar = rightEscapeChar;
//            ParmChar = parmChar;
//        }

//        #region EscapeChar 关键字

//        public string UseEscapeChar(PropertyInfo prop)
//        {
//            return UseEscapeChar(prop.Name);
//        }

//        public string UseEscapeChar(string propStr)
//        {
//            return string.Format("{0}{1}{2}", LeftEscapeChar, propStr, RightEscapeChar);
//        }

//        public List<string> UseEscapeChar(ICollection<string> props)
//        {
//            var result = new List<string>();
//            foreach (var item in props)
//            {
//                result.Add(UseEscapeChar(item));
//            }
//            return result;
//        }

//        public List<string> UseEscapeChar(ICollection<PropertyInfo> props)
//        {
//            var result = new List<string>();
//            foreach (var item in props)
//            {
//                result.Add(UseEscapeChar(item));
//            }
//            return result;
//        }

//        #endregion EscapeChar 关键字

//        #region ParmChar 参数关键字

//        public string UseParmChar(string parm)
//        {
//            return string.Format("{0}{1}", ParmChar, parm);
//        }

//        public string UseParmChar(PropertyInfo parm)
//        {
//            return UseParmChar(parm.Name);
//        }

//        public List<string> UseParmChar(ICollection<PropertyInfo> parms)
//        {
//            var result = new List<string>();
//            foreach (var item in parms)
//            {
//                result.Add(UseParmChar(item.Name));
//            }
//            return result;
//        }

//        public List<string> UseParmChar(ICollection<string> parms)
//        {
//            var result = new List<string>();
//            foreach (var item in parms)
//            {
//                result.Add(UseParmChar(item));
//            }
//            return result;
//        }

//        #endregion ParmChar 参数关键字

//        public List<string> GetEqualConditions(ICollection<PropertyInfo> props)
//        {
//            var result = new List<string>();
//            foreach (var item in props)
//            {
//                result.Add(string.Format("{0} = {1}", UseEscapeChar(item), UseParmChar(item)));
//            }
//            return result;
//        }

//        public List<string> GetEqualConditions(params string[] props)
//        {
//            var result = new List<string>();
//            foreach (var item in props)
//            {
//                result.Add(string.Format("{0} = {1}", UseEscapeChar(item), UseParmChar(item)));
//            }
//            return result;
//        }

//        #region SQL拼接

//        public StringBuilder BuildSelectSqlNoWhere<T>(string tableName, List<PropertyInfo> selectPops)
//        {
//            StringBuilder sb = new StringBuilder();
//            var props = UseEscapeChar(selectPops);
//            sb.AppendFormat("SELECT {0} FROM {1} ", string.Join(",", props), tableName);
//            return sb;
//        }

//        public StringBuilder BuildSelectSql<T>(string tableName, List<PropertyInfo> selectPops)
//        {
//            StringBuilder sb = new StringBuilder();
//            var props = UseEscapeChar(selectPops);
//            sb.AppendFormat("SELECT {0} FROM {1} WHERE 1 = 1 ", string.Join(",", props), tableName);
//            return sb;
//        }

//        public StringBuilder BuildSelectCountSqlNoWhere(string tableName)
//        {
//            return new StringBuilder("SELECT Count(*) FROM " + tableName + " ");
//        }

//        public StringBuilder BuildSelectCountSql(string tableName)
//        {
//            return new StringBuilder("SELECT Count(*) FROM " + tableName + " WHERE 1 = 1");
//        }

//        // ********** ********** ********** **********

//        public StringBuilder BuildInsertSql<T>(string tableName)
//        {
//            StringBuilder sb = new StringBuilder();
//            var properties = Schema.SchemaProvider.GetInsertProperties<T>();
//            var props = UseEscapeChar(properties);
//            var parms = UseParmChar(properties);
//            sb.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2}) ", tableName, string.Join(",", props), string.Join(",", parms));
//            return sb;
//        }

//        // ********** ********** ********** **********

//        public StringBuilder BuildUpdateByIdSql<T>(string tableName, params string[] updateFields)
//        {
//            StringBuilder sb = new StringBuilder();
//            var setValues = GetEqualConditions(updateFields);
//            var whereProp = GetEqualConditions(SchemaProvider.GetPrimaryKey<T>().Name);
//            sb.AppendFormat("UPDATE {0} SET {1} WHERE {2} ", tableName, string.Join(",", setValues), string.Join(" AND ", whereProp));
//            return sb;
//        }

//        // ********** ********** ********** **********

//        public StringBuilder BuildDeleteTableSqlNoWhere(string tableName)
//        {
//            var sb = new StringBuilder();
//            sb.AppendFormat("DELETE FROM {0} ", tableName);
//            return sb;
//        }

//        public StringBuilder BuildDeleteTableSql(string tableName)
//        {
//            var sb = new StringBuilder();
//            sb.AppendFormat("DELETE FROM {0} WHERE 1 = 1 ", tableName);
//            return sb;
//        }

//        public StringBuilder BuildDeleteByIdSql<T>(string tableName)
//        {
//            var sb = BuildDeleteTableSql(tableName);
//            sb.AppendFormat(" AND {0} ", GetEqualConditions(SchemaProvider.GetPrimaryKey<T>().Name).First());
//            return sb;
//        }

//        #endregion SQL拼接

//        public IDataParameter GetParameter<T>(DbCommand cmd, T entity, PropertyInfo propertyInfo)
//        {
//            var result = cmd.CreateParameter();
//            var value = propertyInfo.GetValue(entity, null);

//            result.ParameterName = ParmChar + propertyInfo.Name;
//            result.Direction = ParameterDirection.Input;
//            result.DbType = GetDbType(propertyInfo.PropertyType);
//            result.Value = value;
//            return result;
//        }

//        public DbType GetDbType(Type type)
//        {
//            try
//            {
//                switch (Type.GetTypeCode(type))
//                {
//                    case TypeCode.Boolean: return DbType.Boolean;
//                    case TypeCode.Char:
//                    case TypeCode.SByte:
//                    case TypeCode.Byte: return DbType.Byte;
//                    case TypeCode.Int16:
//                    case TypeCode.UInt16: return DbType.Int16;
//                    case TypeCode.Int32:
//                    case TypeCode.UInt32: return DbType.Int32;
//                    case TypeCode.Int64:
//                    case TypeCode.UInt64: return DbType.Int64;
//                    case TypeCode.Single:
//                    case TypeCode.Double: return DbType.Double;
//                    case TypeCode.Decimal: return DbType.Decimal;
//                    case TypeCode.DateTime: return DbType.DateTime;
//                    case TypeCode.String: return DbType.String;
//                    default: break;
//                }
//                return DbType.String;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"创建 {type.Name} 参数时出错", ex);
//            }
//        }
//    }
//}