using AX.Core.Business.DataModel;
using AX.Core.Extension;
using System;

namespace AX.Core.Business.Managers
{
    public class SystemLogLogic : BaseLogic
    {
        public new static void Log(String content)
        {
            var log = GetDefaultLog();
            log.Content = content;
            GetStaticDB().Insert(log);
        }

        public new static void Log(String type, String content)
        {
            var log = GetDefaultLog();
            log.Type = type;
            log.Content = content;
            GetStaticDB().Insert(log);
        }

        private static Base_SystemLog GetDefaultLog()
        {
            var log = new Base_SystemLog();
            log.CreateTime = DateTime.Now;
            log.CreateDate = DateTime.Now.Date.Format(DateTimeEx.EnumFormatMode.yyyy_MM_dd);
            if (AuthLogic.IsLogin)
            {
                log.UserId = CurrentUser.Id;
                log.UserName = CurrentUser.NickName;
            }
            return log;
        }
    }
}