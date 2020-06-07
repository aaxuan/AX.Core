using System;
using System.Collections.Generic;
using System.Reflection;

namespace AX.Core.DataBase.Configs
{
    /// <summary>
    /// 不同数据库类型的“方言”
    /// </summary>
    public interface IDBDialectConfig
    {
        /// <summary>
        /// 屏蔽数据库关键字左特殊关键字
        /// </summary>
        String LeftEscapeChar { get; }

        /// <summary>
        /// 屏蔽数据库关键字右特殊关键字
        /// </summary>
        String RightEscapeChar { get; }

        /// <summary>
        /// 参数关键字
        /// </summary>
        String DbParmChar { get; }

        /// <summary>
        /// 获取用于判断表是否存在的sql语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        String GetTableExitSql(String tableName, String dataBaseName);

        /// <summary>
        /// 获取用于判断表字段是否存在的sql语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="tableName"></param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        String GetFiledExitSql(String fieldName, String tableName, String dataBaseName);

        /// <summary>
        /// 获取建表语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="KeyName"></param>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        String GetCreateTableSql(String tableName, String KeyName, List<PropertyInfo> propertyInfos);

        /// <summary>
        /// 获取建字段语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        String GetCreateFieldSql(String tableName, PropertyInfo item);
    }
}