using System;

namespace AX.Core.CommonModel
{
    public class ResultMessage
    {
        public ResultMessage()
        { ResultTime = DateTime.Now; }

        public ResultMessage(int code) : this()
        { Code = code; }

        public ResultMessage(int code, string message) : this(code)
        { Message = message; }

        public DateTime ResultTime { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class ResultMessage<T> : ResultMessage
    {
        public ResultMessage() : base()
        { }

        public ResultMessage(T data) : base(200, "请求成功")
        { Data = data; }

        public T Data { get; set; }
    }

    public class RedirectResultMessage : ResultMessage
    {
        public RedirectResultMessage() : base()
        {
            Code = 302;
        }

        public RedirectResultMessage(String url) : base(302, $"请求成功,跳转路径")
        {
            URL = url;
        }

        public String URL { get; set; }
    }
}