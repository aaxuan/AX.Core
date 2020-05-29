using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AX.Core.Business.DataModel
{
    public class Base_File
    {
        [Key]
        [DisplayName("唯一编号")]
        public String Id { get; set; }

        [DisplayName("文件名称")]
        public string Name { get; set; }

        [DisplayName("扩展名")]
        public string Extension { get; set; }

        [DisplayName("文件大小")]
        public string Size { get; set; }

        [DisplayName("创建人Id")]
        public string CreatUserId { get; set; }

        [DisplayName("创建时间")]
        public DateTime? CreatTime { get; set; }

        [DisplayName("二进制体")]
        public byte[] Content { get; set; }
    }
}