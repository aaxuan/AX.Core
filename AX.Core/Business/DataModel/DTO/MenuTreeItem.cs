using System.Collections.Generic;

namespace AX.Core.Business.DataModel.DTO
{
    public class MenuTreeNode
    {
        public Base_Menu Menu { get; set; }

        public List<Base_Menu> Child { get; set; }
    }
}