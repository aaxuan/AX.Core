using AX.Core.Encryption;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AX.Core.Business.DataModel
{
    /// <summary>
    /// 用户属性最简基类
    /// </summary>
    [Table("base_user")]
    public abstract class Base_User
    {
        /// <summary>
        /// 唯一主键
        /// </summary>
        [Key]
        [DisplayName("唯一编号")]
        public String Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [DisplayName("登录名")]
        public string LoginName { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        [DisplayName("加密盐值")]
        public string Salt { get; set; }

        /// <summary>
        /// 密码+_+盐值 加密后密文
        /// </summary>
        [DisplayName("密码+_+盐值 加密后密文")]
        public String Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Gender { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        public string Telephone { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [DisplayName("电子邮件")]
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [DisplayName("QQ")]
        public string QQ { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [DisplayName("微信号")]
        public string WeChat { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [DisplayName("启用")]
        public Boolean IsEnabled { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        [DisplayName("是否超级管理员")]
        public Boolean IsSuperAdmin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public Boolean IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }

        #region 方法

        /// <summary>
        /// 设置盐值
        /// </summary>
        /// <returns></returns>
        public String SetSalt()
        {
            this.Salt = Helper.Rand.NextString(5);
            return this.Salt;
        }

        /// <summary>
        /// 获取密码加盐加密值
        /// </summary>
        /// <param name="passwordValue"></param>
        /// <returns></returns>
        public String GetEncryptedPassword(string passwordValue)
        {
            return MD5.Encrypt($"{passwordValue}_{this.Salt}");
        }

        #endregion 方法
    }
}