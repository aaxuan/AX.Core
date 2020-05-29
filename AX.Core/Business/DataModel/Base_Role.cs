using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AX.Core.Business.DataModel
{
    [DisplayName("角色")]
    [Table("base_Role")]
    public class Role
    {
        [Key]
        [DisplayName("唯一编号")]
        public String Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        public String Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public String Description { get; set; }

        /// <summary>
        /// 包含权限码ID
        /// </summary>
        [DisplayName("包含权限码编号")]
        public String AuthorizationCodeIds { get; set; }

        /// <summary>
        /// 权限码值
        /// </summary>
        [DisplayName("权限码值")]
        public string AuthorizationCodeStrs { get; set; }
    }
}