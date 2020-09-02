using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AX.Core.DataBaseSchema
{
    public class SchemaTable
    {
        public SchemaTable()
        {
            Colmuns = new List<SchemaColumn>();
        }

        [DisplayName("代码名称")]
        public string CodeName { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("包含列")]
        public List<SchemaColumn> Colmuns { get; set; }

        [DisplayName("主键列")]
        public SchemaColumn PrimaryKey { get { return Colmuns.FirstOrDefault(p => p.IsPrimaryKey == true); } }
    }
}