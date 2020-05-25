using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AX.Core.Business.DataModel
{
    [Table("base_systemlog")]
    public class Base_SystemLog
    {
        [Key]
        [DisplayName("唯一编号")]
        public string Id { get; set; }

        [DisplayName("日志内容")]
        public string Content { get; set; }

        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }

        [DisplayName("创建日期")]
        public string CreateDate { get; set; }

        [DisplayName("用户编号")]
        public String UserId { get; set; }

        [DisplayName("用户名称")]
        public String UserName { get; set; }
    }
}