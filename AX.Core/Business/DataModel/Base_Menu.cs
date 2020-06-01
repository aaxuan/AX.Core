using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AX.Core.Business.DataModel
{
    public class Base_Menu
    {
        /// <summary>
        /// 字符串 唯一主键
        /// </summary>
        [Key]
        [DisplayName("唯一编号")]
        public String Id { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        [DisplayName("父级主键")]
        public String PId { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        [DisplayName("图标样式")]
        public String IconClass { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [DisplayName("菜单名称")]
        public String Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [DisplayName("路径")]
        public String URL { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [DisplayName("排序号")]
        public Int32 Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public String Notes { get; set; }

        /// <summary>
        /// 仅系统管理员可见
        /// </summary>
        [DisplayName("仅系统管理员可见")]
        public bool OnlyAdmin { get; set; }

        /// <summary>
        /// 权限码编号
        /// </summary>
        [DisplayName("权限码编号")]
        public String AuthCodeId { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        [DisplayName("权限码")]
        public String AuthCode { get; set; }
    }
}