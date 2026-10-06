using System;
using System.Web;

public static class UserInfoHelper
{
    public static string GetClientIp()
    {
        string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
    }

    public static string GetBrowserInfo()
    {
        var browser = HttpContext.Current.Request.Browser;
        return $"{browser.Browser} {browser.Version}";
    }

    public static string GetPlatform()
    {
        return HttpContext.Current.Request.Browser.Platform;
    }

    public static string GetUserAgent()
    {
        return HttpContext.Current.Request.UserAgent;
    }

    public static string GetOS()
    {
        string userAgent = GetUserAgent().ToLower();

        if (userAgent.Contains("windows nt 10.0"))
            return "Windows 10";
        else if (userAgent.Contains("windows nt 6.3"))
            return "Windows 8.1";
        else if (userAgent.Contains("windows nt 6.2"))
            return "Windows 8";
        else if (userAgent.Contains("windows nt 6.1"))
            return "Windows 7";
        else if (userAgent.Contains("macintosh"))
            return "Mac OS";
        else if (userAgent.Contains("android"))
            return "Android";
        else if (userAgent.Contains("iphone"))
            return "iOS";
        else if (userAgent.Contains("linux"))
            return "Linux";
        else
            return "Unknown OS";
    }
}
