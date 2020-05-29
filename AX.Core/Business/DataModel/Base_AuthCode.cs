using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AX.Core.Business.DataModel
{
    [DisplayName("权限码")]
    internal class Base_AuthCode
    {
        [Key]
        [DisplayName("唯一编号")]
        public String Id { get; set; }

        /// <summary>
        /// 权限码值
        /// </summary>
        [DisplayName("权限码值")]
        public String Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public String Description { get; set; }
    }
}