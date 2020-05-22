using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AX.Core.Business.DataModel
{
    [Table("base_systemlog")]
    public class BaseSystemLog
    {
        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public String UserId { get; set; }

        public String UserName { get; set; }
    }
}