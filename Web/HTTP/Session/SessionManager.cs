using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Cookies;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.Http.View;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.Session
{
    public static class SessionManager
    {
        private static IUserService userService;
        private const string UserSession = "USER_SESSION";
        private const string LocaleSession = "LOCALE_SESSION";

        static SessionManager()
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["ManagerIoC"];
            userService = iocManager.Resolve<IUserService>();
        }

        #region UserService

        public static void TouchSession(HttpContext context)
        {
            if (!IsUserAuthenticated(context))
            {
                if (context.Request.Cookies is null)
                {
                    return;
                }

                string login = CookiesManager.GetLogin(context);
                string encryptedPassword = CookiesManager.GetEncryptedPassword(context);

                if (login is null || encryptedPassword is null)
                {
                    return;
                }

                try
                {
                    LoginResult loginResult = userService.Login(login, encryptedPassword, true);
                    context.Session.Add(UserSession, loginResult.UserProfileId);
                    context.Session.Add(LocaleSession, new Locale(loginResult.Language, loginResult.Country));

                    FormsAuthentication.SetAuthCookie(login, true);
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        public static void RegisterUser(HttpContext context, string login, string pass, UserProfileDetails userProfileDetails)
        {
            long userProfileId = userService.RegisterUser(login, pass, userProfileDetails);
            context.Session.Add(UserSession, userProfileId);
            context.Session.Add(LocaleSession, new Locale(userProfileDetails.Language, userProfileDetails.Country));

            FormsAuthentication.SetAuthCookie(userProfileDetails.LoginName, false);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        public static void Login(HttpContext context, string login, string password, bool remember)
        {
            LoginResult loginResult = userService.Login(login, password, false);
            context.Session.Add(UserSession, loginResult.UserProfileId);
            context.Session.Add(LocaleSession, new Locale(loginResult.Language, loginResult.Country));

            if (remember)
            {
                CookiesManager.LeaveCookies(context, login, loginResult.EncryptedPassword);
            }
        }

        public static void Logout(HttpContext context)
        {
            if (IsUserAuthenticated(context))
            {
                CookiesManager.RemoveCookies(context);
                context.Session.Abandon();
                FormsAuthentication.SignOut();
            }
        }

        public static bool IsUserAuthenticated(HttpContext context)
        {
            if (context.Session is null)
            {
                return false;
            }
            else
            {
                return !(context.Session[UserSession] is null);
            }
        }

        /// <exception cref="AuthenticationException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public static UserProfileDetails FindUser(HttpContext context)
        {
            if (!IsUserAuthenticated(context))
            {
                throw new AuthenticationException();
            }

            return userService.FindUserProfileDetails((long)context.Session[UserSession]);
        }


        /// <exception cref="AuthenticationException"/>
        /// <exception cref="InputValidationException"/>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        public static void ChangePassword(HttpContext context, string oldPassword, string newPassword)
        {
            if (!IsUserAuthenticated(context))
            {
                throw new AuthenticationException();
            }

            userService.ChangePassword((long)context.Session[UserSession], oldPassword, newPassword);

            CookiesManager.RemoveCookies(context);
        }

        public static Locale GetLocale(HttpContext context)
        {
            if (IsUserAuthenticated(context))
            {
                return (Locale)context.Session[LocaleSession];
            }
            else
            {
                return new Locale();
            }
        }

        #endregion UserService

        public static long getUserId(HttpContext context)
        {
            return (long)context.Session[UserSession];
        }

    }

}