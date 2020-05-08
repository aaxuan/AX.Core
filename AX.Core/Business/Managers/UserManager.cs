using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using AX.Core.DataBase;
using AX.Core.Encryption;
using AX.Core.Helper;

namespace AX.Core.Business.Managers
{
    /// <summary>
    /// 用户逻辑基类
    /// </summary>
    public class BaseUserManager
    {
        public virtual AXDataBase DB { get; set; }

        public virtual BaseUser CheckUserLogin(string loginName, string password)
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
            { throw new AXWarringMesssageException("登录名和密码不能为空"); }
            var user = DB.SingleOrDefault<BaseUser>("where loginname = @LoginName", loginName);
            if (user == null)
            { throw new AXWarringMesssageException("用户不存在"); }
            if (user.GetEncryptedPassword(password) != user.Password)
            { throw new AXWarringMesssageException("密码错误，请检查后重新登录"); }
            if (user.IsEnable == false)
            { throw new AXWarringMesssageException("账号已被禁用，请联系系统管理员"); }
            return user;
        }

        public virtual BaseUser New(BaseUser user)
        {
            if (string.IsNullOrWhiteSpace(user.LoginName))
            { throw new AXWarringMesssageException("用户登陆名不能为空"); }
            if (string.IsNullOrWhiteSpace(user.NickName))
            { throw new AXWarringMesssageException("用户昵称不能为空"); }
            if (string.IsNullOrWhiteSpace(user.Password))
            { throw new AXWarringMesssageException("用户密码不能为空"); }

            DB.GetCount<BaseUser>("where loginname = @LoginName", user.LoginName);
            if (user != null)
            { throw new AXWarringMesssageException("用户名已被使用"); }

            user.SetSalt();
            user.Password = user.GetEncryptedPassword(user.Password);
            user.IsEnable = true;

            return DB.Insert(user);
        }

        public virtual string RandomPassword(string Id)
        {
            var user = DB.SingleOrDefault<BaseUser>(Id);
            var newPassword = Rand.NextString(8);
            user.Password = MD5.Encrypt(newPassword + "_" + user.Salt);
            DB.Update<BaseUser>(user, "Password");
            return newPassword;
        }
    }
}