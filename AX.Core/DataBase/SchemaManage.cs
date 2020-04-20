using AX.Core.CommonModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace AX.Core.DataBase
{
    public static class SchemaManage
    {
        /// <summary>
        /// 获取实体表名称
        /// 尝试 System.ComponentModel.DataAnnotations.Schema.TableAttribute 特性
        /// 没有则取实体类名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTableName<T>()
        {
            var result = string.Empty;
            var tableattr = typeof(T)
            .GetCustomAttributes(true)
            .SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as TableAttribute;

            if (tableattr != null)
            { return tableattr.Name; }

            return typeof(T).Name;
        }

        /// <summary>
        /// 获取实体表主键字段
        /// 尝试 System.ComponentModel.DataAnnotations.KeyAttribute 特性
        /// 没有则取名称为Id的字段 位取到则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo GetPrimaryKey<T>()
        {
            PropertyInfo prop = null;
            var allProperties = typeof(T).GetProperties().ToList();

            var primaryKey = allProperties.SingleOrDefault(p => p.GetCustomAttribute(typeof(KeyAttribute)) != null);
            if (primaryKey != null)
            { return primaryKey; }

            //若没有特性标注 则默认寻找Id名称属性 默认不自增
            prop = allProperties.FirstOrDefault(p => p.Name.Equals("Id"));
            if (prop != null)
            { return prop; }

            throw new AXDataBaseException($"【{typeof(T).FullName}】 未找到主键标注", string.Empty);
        }

        //public String GetSetSchemaSql<T>(bool execute) where T : class
        //{
        //    var result = new StringBuilder();
        //    var tableName = typeof(T).Name.ToLower();
        //    var keyName = GetPrimaryKeyName<T>();
        //    var fields = typeof(T).GetProperties();

        //    //判断表是否存在 不存在则新建
        //    var exitSql = $"SELECT COUNT(*) FROM information_schema.`TABLES` WHERE TABLE_NAME = '{tableName}' AND TABLE_SCHEMA = '{_connection.Database.ToLower()}'";
        //    if (_connection.ExecuteScalar<int>(exitSql) <= 0)
        //    {
        //        result.Append($"CREATE TABLE IF NOT EXISTS {tableName} (");
        //        for (int i = 0; i < fields.Length; i++)
        //        {
        //            var item = fields[i];
        //            result.Append($"{item.Name.ToLower()} {GetType(item)}");
        //            if (i != fields.Length)
        //            { result.Append($","); }
        //        }
        //        result.Append($"PRIMARY KEY({keyName})");
        //        result.Append($")");
        //        result.Append($"ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COMMENT = '{tableName}';");
        //    }
        //    else
        //    {
        //        for (int i = 0; i < fields.Length; i++)
        //        {
        //            var item = fields[i];
        //            var filedExitSql = $"SELECT COUNT(*) FROM information_schema.`COLUMNS` WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{item.Name.ToLower()}' AND TABLE_SCHEMA = '{_connection.Database.ToLower()}';";

        //            if (_connection.ExecuteScalar<int>(filedExitSql) <= 0)
        //            {
        //                result.Append($"ALTER TABLE {tableName} ADD COLUMN {item.Name.ToLower()} {GetType(item)} DEFAULT NULL;");
        //            }
        //        }
        //    }

        //    if (execute && result.Length > 0)
        //    { _connection.Execute(result.ToString()); }
        //    return result.ToString();
        //}

        //protected string GetType(PropertyInfo item)
        //{
        //    //数值类
        //    if (item.PropertyType.FullName == typeof(int).FullName)
        //    { return "int(11)"; }
        //    if (item.PropertyType.FullName == typeof(double).FullName)
        //    { return "double"; }
        //    if (item.PropertyType.FullName == typeof(bool).FullName)
        //    { return "bit(1)"; }
        //    if (item.PropertyType.FullName == typeof(decimal).FullName)
        //    { return "decimal(10, 2)"; }

        //    //时间
        //    if (item.PropertyType.FullName == typeof(DateTime?).FullName)
        //    { return "datetime"; }

        //    //字符串
        //    if (item.PropertyType.FullName == typeof(string).FullName)
        //    { return "varchar(255)"; }

        //    return "未匹配类型";
        //}
    }
}