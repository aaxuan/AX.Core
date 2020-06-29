using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using System;

namespace AX.Core.Business.Managers
{
    public class AuthLogic : BaseLogic
    {
        public static Func<Base_User> GetCurrentUserFunc;
        public static Action SetUserToken;
        public static Action ClearUserToken;

        public static bool IsLogin
        {
            get
            {
                var user = GetCurrentUser();
                if (user == null)
                { return false; }
                return true;
            }
        }

        public static Base_User GetCurrentUser()
        {
            return GetCurrentUserFunc();
        }

        public Base_User LoginIn(string loginName, string passWord)
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(passWord))
            { throw new AXWarringMesssageException("登录名和密码不能为空"); }

            var user = DB.SingleOrDefault<Base_User>("where loginname = @LoginName", loginName);

            if (user == null)
            { throw new AXWarringMesssageException("用户不存在"); }

            if (user.IsEnabled == false)
            { throw new AXWarringMesssageException("账号已被禁用，请联系系统管理员"); }

            if (user.GetEncryptedPassword(passWord) != user.Password)
            { throw new AXWarringMesssageException("密码错误，请检查后重新登录"); }

            //发放令牌
            SetUserToken();

            ////设置 Cookie
            //HttpCookie cookie = new HttpCookie(BASE_AUTHCOOKIE_NAME);
            //cookie.HttpOnly = true;
            //cookie.Values["name"] = user.NickName;
            //cookie.Values["token"] = AX.Core.Encryption.AES.Encrypt(string.Format($"{user.Id}.{user.Salt}"));
            //HttpContext.Current.Response.AppendCookie(cookie);

            //记录日志
            Log("登录系统", string.Format("【{0}】登陆系统", user.NickName));
            return user;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public void LoginOut()
        {
            ClearUserToken();

            //HttpCookie cookie = new HttpCookie(BASE_AUTHCOOKIE_NAME);
            //cookie.Expires = DateTime.Now.AddDays(-1);
            //HttpContext.Current.Response.AppendCookie(cookie);
            //return true;

            Log("退出系统", string.Format("【{0}】退出系统", GetCurrentUser().NickName));
        }
    }
}