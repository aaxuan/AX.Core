using System;

namespace AX.Core.CommonModel.Exceptions
{
    public class AXCoreException : System.Exception
    {
        public DateTime ThrowTime { get; set; }

        /// <summary>
        /// 框架基础异常
        /// </summary>
        /// <param name="errMessage"></param>
        public AXCoreException(string errMessage) : base(errMessage)
        {
            ThrowTime = DateTime.Now;
        }
    }
}