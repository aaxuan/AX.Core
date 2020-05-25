using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using AX.Core.Encryption;
using AX.Core.Helper;

namespace AX.Core.Business.Managers
{
    /// <summary>
    /// 用户逻辑基类
    /// </summary>
    public class UserLogic : BaseLogic
    {
        public virtual Base_User New(Base_User user)
        {
            if (string.IsNullOrWhiteSpace(user.LoginName))
            { throw new AXWarringMesssageException("用户登陆名不能为空"); }
            if (string.IsNullOrWhiteSpace(user.NickName))
            { throw new AXWarringMesssageException("用户昵称不能为空"); }
            if (string.IsNullOrWhiteSpace(user.Password))
            { throw new AXWarringMesssageException("用户密码不能为空"); }

            GetStaticDB().GetCount<Base_User>("where loginname = @LoginName", user.LoginName);
            if (user != null)
            { throw new AXWarringMesssageException("用户名已被使用"); }

            user.SetSalt();
            user.Password = user.GetEncryptedPassword(user.Password);
            user.IsEnable = true;

            return GetStaticDB().Insert(user);
        }

        public virtual string RandomPassword(string id)
        {
            var user = GetStaticDB().CheckById<Base_User>(id);
            var newPassword = Rand.NextString(8);
            user.Password = MD5.Encrypt(newPassword + "_" + user.Salt);
            GetStaticDB().Update<Base_User>(user, "Password");
            return newPassword;
        }
    }
}