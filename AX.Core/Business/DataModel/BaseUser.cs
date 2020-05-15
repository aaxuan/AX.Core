using AX.Core.Encryption;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AX.Core.Business.DataModel
{
    /// <summary>
    /// 用户属性最简基类
    /// </summary>
    [Table("base_user")]
    public abstract class BaseUser
    {
        /// <summary>
        /// 字符串 唯一主键
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        public String LoginName { get; set; }

        /// <summary>
        /// 昵称名称
        /// </summary>
        public String NickName { get; set; }

        /// <summary>
        /// 密码加密盐值
        /// </summary>
        public String Salt { get; set; }

        /// <summary>
        /// 密码+_+盐值 加密后密文
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Boolean IsSuperAdmin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Boolean IsEnable { get; set; }

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
    }
}