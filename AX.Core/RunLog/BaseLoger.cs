using System;
using System.Text;

namespace AX.Core.RunLog
{
    //https://www.cnblogs.com/emrys5/p/flashlog.html
    //https://www.cnblogs.com/xcj26/p/6037808.html

    public abstract class BaseLoger
    {
        public BaseLoger()
        {
        }

        internal StringBuilder AllLog { get; set; } = new StringBuilder();

        internal string CreateLogMsg(string msg)
        {
            var result = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] { msg }";
            if (UseLogLog) { AllLog.AppendLine(result); }
            return (result);
        }

        public bool UseLogLog { get; set; } = false;

        public String GetAllLog()
        {
            return AllLog.ToString();
        }

        public void ClearAllLog()
        {
            AllLog = new StringBuilder();
        }

        #region 子类实现

        public abstract void Info(string msg);

        public abstract void Err(string msg);

        public abstract void Waring(string msg);

        public abstract void Line();

        public abstract void EmptyLine();

        #endregion 子类实现
    }
}