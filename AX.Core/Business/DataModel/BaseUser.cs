using System;
using System.ComponentModel.DataAnnotations;

namespace AX.Core.Business.DataModel
{
    /// <summary>
    /// 用户属性最简基类
    /// </summary>
    public interface IUser
    {
        [Key]
        String Id { get; set; }

        String LoginName { get; set; }

        String Salt { get; set; }

        String Password { get; set; }

        Boolean IsSuperAdmin { get; set; }

        bool IsEnable { get; set; }
    }
}