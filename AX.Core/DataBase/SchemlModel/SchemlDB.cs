using System.Collections.Generic;
using System.ComponentModel;

namespace AX.Core.DataBase.SchemlModel
{
    public class SchemlDB
    {
        public SchemlDB()
        {
            Tables = new List<SchemlTable>();
        }

        [DisplayName("代码名称")]
        public string CodeName { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("包含表")]
        public List<SchemlTable> Tables { get; set; }
    }
}