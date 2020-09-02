using System.ComponentModel;

namespace AX.Core.DataBaseSchema
{
    public class SchemaColumn
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