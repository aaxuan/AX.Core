using System.Collections.Generic;
using System.Reflection;

namespace AX.Core.DataBase
{
    public interface IAdapter
    {
        string DbParmChar { get; }
        string LeftEscapeChar { get; }
        string RightEscapeChar { get; }

        string GetColumnExitSql(string fieldName, string tableName, string dataBaseName);

        string GetCreateColumnSql(string tableName, PropertyInfo item);

        string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos);

        string GetTableExitSql(string tableName, string dataBaseName);
    }
}