using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

    [Serializable]
    public class SchemaTable
    {
        public SchemaTable()
        {
            Colmuns = new List<SchemaColmun>();
        }

        [DisplayName("代码名称")]
        public string CodeName { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("包含列")]
        public List<SchemaColmun> Colmuns { get; set; }

        [DisplayName("主键列")]
        public SchemaColmun PrimaryKey { get { return Colmuns.FirstOrDefault(p => p.IsPrimaryKey == true); } }
    }

    [Serializable]
    public class SchemaColmun
    {
        [DisplayName("代码名称")]
        public string CodeName { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("是否可空")]
        public bool CanNullable { get; set; }

        [DisplayName("是否主键")]
        public bool IsPrimaryKey { get; set; }

        [DisplayName("默认值")]
        public string DefaultValue { get; set; }

        [DisplayName("位数")]
        public int Scale { get; set; }

        [DisplayName("精度")]
        public int Precision { get; set; }

        [DisplayName("长度")]
        public int Length { get; set; }

        [DisplayName("自增")]
        public bool AutoIncrement { get; set; }

        [DisplayName("数据库类型")]
        public string DBType { get; set; }

        [DisplayName("排序号")]
        public int Order { get; set; }
    }
}