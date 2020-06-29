using System;
using System.Collections.Generic;
using System.ComponentModel;
using static AX.Core.DataBase.DBFactory;

namespace AX.Core.DataBase.Schema
{
    [Serializable]
    public class SchemaDB
    {
        public SchemaDB()
        {
            Tables = new List<SchemaTable>();
        }

        [DisplayName("代码名称")]
        public string CodeName { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("包含表")]
        public List<SchemaTable> Tables { get; set; }

        [DisplayName("数据库连接串")]
        public string ConnectionString { get; set; }

        [DisplayName("数据库类型")]
        public DataBaseType DBType { get; set; }
    }
}