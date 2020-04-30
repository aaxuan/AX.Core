using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using AX.Core.DataBase;
using AX.Core.Encryption;
using AX.Core.Helper;

namespace AX.Core.Business.Managers
{
    public abstract class UserManager
    {
        public virtual AXDataBase DB { get; set; }

        public virtual IUser LoginCheck(string loginName, string password)
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
            { throw new AXWarringMesssageException("请输入登录名和密码"); }
            var user = DB.SingleOrDefault<IUser>("where loginname = @LoginName", loginName);
            if (user == null)
            { throw new AXWarringMesssageException("登录名或密码错误"); }

            //比较密码加盐后加密值
            password = MD5.Encrypt(password + "_" + user.Salt);
            if (password != user.Password)
            { throw new AXWarringMesssageException("登录名或密码错误"); }

            if (user.IsEnable == false)
            { throw new AXWarringMesssageException("您的账号已被禁用，请联系系统管理员"); }

            return user;
        }

        public virtual IUser Add(IUser user)
        {
            if (string.IsNullOrWhiteSpace(user.LoginName))
            { throw new AXWarringMesssageException("用户登陆名不能为空"); }
            if (string.IsNullOrWhiteSpace(user.Password))
            { throw new AXWarringMesssageException("用户密码不能为空"); }

            user.Salt = Rand.NextString(5);
            user.Password = MD5.Encrypt(user.Password + "_" + user.Salt);
            user.IsEnable = true;
            return DB.Insert(user);
        }

        public virtual string RandomPassword(string Id)
        {
            var user = DB.SingleOrDefault<IUser>(Id);
            var newPassword = Rand.NextString(8);
            user.Password = MD5.Encrypt(newPassword + "_" + user.Salt);
            DB.Update<IUser>(user, "Password");
            return newPassword;
        }
    }
}