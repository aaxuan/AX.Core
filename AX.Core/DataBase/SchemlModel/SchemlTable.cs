using System.Collections.Generic;
using System.ComponentModel;

namespace AX.Core.DataBase.SchemlModel
{
    public class SchemlTable
    {
        public SchemlTable()
        {
            Colmuns = new List<SchemlColmun>();
        }

        [DisplayName("代码名称")]
        public string CodeName { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("包含列")]
        public List<SchemlColmun> Colmuns { get; set; }
    }
}