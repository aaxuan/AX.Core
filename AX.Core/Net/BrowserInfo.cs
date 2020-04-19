using System;
using System.Web;

namespace AX.Framework.Net
{
    public static class BrowserInfo
    {
        ///// <summary>
        ///// 获取客户端操作系统版本
        ///// </summary>
        ///// <returns></returns>
        //public static string GetOSName()
        //{
        //    if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.UserAgent == null)
        //    { return "未知"; }

        //    var userAgent = HttpContext.Current.Request.UserAgent.ToLower();

        //    if (userAgent.Contains("android"))
        //    { return "Android"; }
        //    else if (userAgent.Contains("mac os x"))
        //    { return "ios"; }
        //    else if (userAgent.Contains("windows phone"))
        //    { return "Windows Phone"; }
        //    else if (userAgent.Contains("nt 10.0"))
        //    { return "Windows 10"; }
        //    else if (userAgent.Contains("NT 6.3"))
        //    { return "Windows8.1"; }
        //    else if (userAgent.Contains("NT 6.2"))
        //    { return "Windows8"; }
        //    else if (userAgent.Contains("nt 6.1"))
        //    { return "Windows 7"; }
        //    else if (userAgent.Contains("nt 6.0"))
        //    { return "Windows Vista/Server 2008"; }
        //    else if (userAgent.Contains("nt 5.2"))
        //    { return "Windows Server 2003"; }
        //    else if (userAgent.Contains("nt 5.1"))
        //    { return "Windows XP"; }
        //    else if (userAgent.Contains("nt 5"))
        //    { return "Windows 2000"; }
        //    else if (userAgent.Contains("nt 4"))
        //    { return "Windows NT4"; }
        //    else if (userAgent.Contains("me"))
        //    { return "Windows Me"; }
        //    else if (userAgent.Contains("98"))
        //    { return "Windows 98"; }
        //    else if (userAgent.Contains("95"))
        //    { return "Windows 95"; }
        //    else if (userAgent.Contains("mac"))
        //    { return "Mac"; }
        //    else if (userAgent.Contains("unix"))
        //    { return "UNIX"; }
        //    else if (userAgent.Contains("linux"))
        //    { return "Linux"; }
        //    else if (userAgent.Contains("sunos"))
        //    { return "SunOS"; }

        //    return "未知";
        //}

        ///// <summary>
        ///// 获取浏览器名称
        ///// </summary>
        ///// <returns></returns>
        //public static string GetBrowser()
        //{
        //    if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.UserAgent == null)
        //    { return "未知"; }

        //    var userAgent = HttpContext.Current.Request.UserAgent.ToLower();

        //    if (userAgent.Contains("opera/ucweb"))
        //    { return "UC Opera"; }
        //    else if (userAgent.Contains("openwave/ ucweb"))
        //    { return "UCOpenwave"; }
        //    else if (userAgent.Contains("ucweb"))
        //    { return "UC"; }
        //    else if (userAgent.Contains("360se"))
        //    { return "360"; }
        //    else if (userAgent.Contains("metasr"))
        //    { return "搜狗"; }
        //    else if (userAgent.Contains("maxthon"))
        //    { return "遨游"; }
        //    else if (userAgent.Contains("the world"))
        //    { return "世界之窗"; }
        //    else if (userAgent.Contains("tencenttraveler") || userAgent.Contains("qqbrowser"))
        //    { return "腾讯"; }
        //    else if (userAgent.Contains("chrome"))
        //    { return "Chrome"; }
        //    else if (userAgent.Contains("safari"))
        //    { return "safari"; }
        //    else if (userAgent.Contains("firefox"))
        //    { return "Firefox"; }
        //    else if (userAgent.Contains("opera"))
        //    { return "Opera"; }
        //    else if (userAgent.Contains("msie"))
        //    { return "IE"; }
        //    else
        //    { return System.Web.HttpContext.Current.Request.Browser.Browser; }
        //}

        ///// <summary>
        ///// 取得客户端IP
        ///// </summary>
        ///// <returns></returns>
        //public static string GetClientIp()
        //{
        //    var result = "0.0.0.0";

        //    if (HttpContext.Current == null && HttpContext.Current.Request == null)
        //    { return result; }

        //    result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (result == null || result == String.Empty)
        //    {
        //        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    if (result == null || result == String.Empty)
        //    {
        //        result = HttpContext.Current.Request.UserHostAddress;
        //    }
        //    if (result == null || result == String.Empty)
        //    {
        //        result = "0.0.0.0";
        //    }

        //    return result;
        //}
    }
}