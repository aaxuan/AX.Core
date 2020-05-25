namespace AX.Core.Net
{
    public static class BrowserInfo
    {
        /// <summary>
        /// 获取客户端操作系统版本
        /// HttpContext.Current.Request.UserAgent
        /// </summary>
        /// <returns></returns>
        public static string GetOSName(string userAgent)
        {
            if (userAgent.Contains("android"))
            { return "Android"; }
            if (userAgent.Contains("mac os x"))
            { return "ios"; }
            if (userAgent.Contains("windows phone"))
            { return "Windows Phone"; }
            if (userAgent.Contains("nt 10.0"))
            { return "Windows 10"; }
            if (userAgent.Contains("NT 6.3"))
            { return "Windows8.1"; }
            if (userAgent.Contains("NT 6.2"))
            { return "Windows8"; }
            if (userAgent.Contains("nt 6.1"))
            { return "Windows 7"; }
            if (userAgent.Contains("nt 6.0"))
            { return "Windows Vista/Server 2008"; }
            if (userAgent.Contains("nt 5.2"))
            { return "Windows Server 2003"; }
            if (userAgent.Contains("nt 5.1"))
            { return "Windows XP"; }
            if (userAgent.Contains("nt 5"))
            { return "Windows 2000"; }
            if (userAgent.Contains("nt 4"))
            { return "Windows NT4"; }
            if (userAgent.Contains("me"))
            { return "Windows Me"; }
            if (userAgent.Contains("98"))
            { return "Windows 98"; }
            if (userAgent.Contains("95"))
            { return "Windows 95"; }
            if (userAgent.Contains("mac"))
            { return "Mac"; }
            if (userAgent.Contains("unix"))
            { return "UNIX"; }
            if (userAgent.Contains("linux"))
            { return "Linux"; }
            if (userAgent.Contains("sunos"))
            { return "SunOS"; }

            return "未知";
        }

        /// <summary>
        /// 获取浏览器名称
        /// HttpContext.Current.Request.UserAgent
        /// </summary>
        /// <returns></returns>
        public static string GetBrowser(string userAgent)
        {
            if (userAgent.Contains("opera/ucweb"))
            { return "UC Opera"; }
            if (userAgent.Contains("openwave/ ucweb"))
            { return "UCOpenwave"; }
            if (userAgent.Contains("ucweb"))
            { return "UC"; }
            if (userAgent.Contains("360se"))
            { return "360"; }
            if (userAgent.Contains("metasr"))
            { return "搜狗"; }
            if (userAgent.Contains("maxthon"))
            { return "遨游"; }
            if (userAgent.Contains("the world"))
            { return "世界之窗"; }
            if (userAgent.Contains("tencenttraveler") || userAgent.Contains("qqbrowser"))
            { return "腾讯"; }
            if (userAgent.Contains("chrome"))
            { return "Chrome"; }
            if (userAgent.Contains("safari"))
            { return "safari"; }
            if (userAgent.Contains("firefox"))
            { return "Firefox"; }
            if (userAgent.Contains("opera"))
            { return "Opera"; }
            if (userAgent.Contains("msie"))
            { return "IE"; }

            return "未知";
        }

        ///// <summary>
        ///// 获得客户端IP
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