using System;

namespace AX.Core.RunLog
{
    public abstract class BaseLoger
    {
        public BaseLoger()
        {
        }

        internal string CreateLogMsg(string msg)
        {
            return ($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] { msg }");
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