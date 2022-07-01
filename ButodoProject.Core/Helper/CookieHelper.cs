
using System;


namespace FikirgenCms.Core.Helper
{

    public class CookieHelper
    {
        //public static string GetCookie(string cookieName)
        //{
        //    var myCookie = HttpContext.Current.Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
        //    var cookie = myCookie.Value;

        //    return cookie == null ? null : myCookie[cookieName];
        //}

        //public static HttpCookie GetCookies(string cookieName)
        //{
        //    var myCookie = HttpContext.Current.Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
        //    var cookie = myCookie;

        //    return cookie;

        //}

        //public static bool SetCookie(string cookieName, string value, bool isLongTerm)
        //{
        //    var success = true;
        //    try
        //    {
        //        var myCookie = HttpContext.Current.Request.Cookies[cookieName] ?? new HttpCookie(cookieName);

        //        myCookie[cookieName] = value;

        //        myCookie.Expires = isLongTerm ? DateTime.Now.AddYears(1) : DateTime.Now.AddHours(2);
        //        HttpContext.Current.Response.Cookies.Add(myCookie);
        //    }
        //    catch (Exception)
        //    {

        //        success = false;
        //    }
        //    return success;
        //}

        //public static HttpCookie SetCookie(HttpCookie cookie)
        //{
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //    //HttpContext.Current.Response.Cookies.Set(cookie);
        //    return cookie;
        //}

        //public static bool DeleteCookie(string cookieName)
        //{
        //    var myCookie = HttpContext.Current.Request.Cookies[cookieName];
        //    if (myCookie == null)
        //        return false;

        //    myCookie.Expires = DateTime.Now.AddHours(-24);
        //    HttpContext.Current.Response.Cookies.Add(myCookie);

        //    return true;
        //}


    }

}