using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AX.Core.Business.DataModel
{
    [DisplayName("用户角色绑定关系表")]
    [Table("base_userrolemap")]
    internal class Base_UserRoleMap
    {
        [Key]
        [DisplayName("唯一编号")]
        public String Id { get; set; }

        [DisplayName("用户编号")]
        public String UserId { get; set; }

        [DisplayName("角色编号")]
        public String RoleId { get; set; }
    }
}