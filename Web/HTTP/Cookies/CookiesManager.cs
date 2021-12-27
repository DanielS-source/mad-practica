using System;
using System.Web;
using System.Web.Security;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.Cookies
{
    public static class CookiesManager
    {
        private const string LoginNameCookie = "LOGIN_COOKIE";
        private const string EncryptedPasswordNameCookie = "ENCRYPTED_PASSWORD_COOKIE";
        private const int ExpirationTime = 30 * 24 * 3600;

        public static void LeaveCookies(HttpContext context, String login, String password)
        {
            HttpCookie loginCookie = new HttpCookie(LoginNameCookie, login)
            {
                Expires = DateTime.Now.AddSeconds(ExpirationTime)
            };
            context.Response.Cookies.Add(loginCookie);

            HttpCookie encryptedPasswordCookie = new HttpCookie(EncryptedPasswordNameCookie, password)
            {
                Expires = DateTime.Now.AddSeconds(ExpirationTime)
            };
            context.Response.Cookies.Add(encryptedPasswordCookie);

            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(login, true);
            authCookie.Expires = DateTime.Now.AddSeconds(ExpirationTime);
        }

        public static void RemoveCookies(HttpContext context)
        {
            HttpCookie loginCookie = new HttpCookie(LoginNameCookie, "")
            {
                Expires = DateTime.Now.AddSeconds(0)
            };
            context.Response.Cookies.Add(loginCookie);

            HttpCookie encryptedPasswordCookie = new HttpCookie(EncryptedPasswordNameCookie, "")
            {
                Expires = DateTime.Now.AddSeconds(0)
            };
            context.Response.Cookies.Add(encryptedPasswordCookie);
        }

        public static string GetLogin(HttpContext context)
        {
            HttpCookie loginCookie = context.Request.Cookies.Get(LoginNameCookie);

            if (loginCookie is null)
            {
                return null;
            }
            else
            {
                return loginCookie.Value;
            }
        }

        public static string GetEncryptedPassword(HttpContext context)
        {
            HttpCookie encryptedPasswordCookie = context.Request.Cookies.Get(EncryptedPasswordNameCookie);

            if (encryptedPasswordCookie is null)
            {
                return null;
            }
            else
            {
                return encryptedPasswordCookie.Value;
            }
        }
    }
}