using System;

namespace AX.Core.CommonModel
{
    /// <summary>
    /// 基类
    /// </summary>
    public class ResultMessage
    {
        public ResultMessage()
        { }

        public ResultMessage(int code)
        { Code = code; }

        public ResultMessage(int code, string message) : this(code)
        { Message = message; }

        public int Code { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// 泛型 数据类型 返回信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultMessage<T> : ResultMessage
    {
        public ResultMessage()
        { }

        public ResultMessage(T data) : base(200, "请求成功")
        { Data = data; }

        public T Data { get; set; }
    }

    /// <summary>
    /// 跳转 返回信息
    /// </summary>
    public class RedirectResultMessage : ResultMessage
    {
        public RedirectResultMessage()
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