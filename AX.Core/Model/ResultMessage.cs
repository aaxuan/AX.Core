using System;

namespace AX.Core.CommonModel
{
    public class ResultMessage
    {
        public ResultMessage()
        { }

        public ResultMessage(int code)
        { Code = code; }

        public ResultMessage(int code, string message) : this(code)
        { Message = message; }

        public DateTime ResultTime { get; set; } = DateTime.Now;

        public int Code { get; set; } = 200;

        public string Message { get; set; } = "请求成功";
    }

    public class ResultMessageT<T> : ResultMessage
    {
        public ResultMessageT() : base()
        { }

        public ResultMessageT(T data) : base()
        { Data = data; }

        public T Data { get; set; }
    }
}