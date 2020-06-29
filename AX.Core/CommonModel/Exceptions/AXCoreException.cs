using System;

namespace AX.Core.CommonModel.Exceptions
{
    public class AXCoreException : System.Exception
    {
        public DateTime ThrowTime { get; set; }

        public AXCoreException(string errMessage) : base(errMessage)
        {
            ThrowTime = DateTime.Now;
        }
    }
}