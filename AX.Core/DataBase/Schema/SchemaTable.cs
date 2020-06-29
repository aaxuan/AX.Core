using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AX.Core.DataBase.Schema
{
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
    }
}