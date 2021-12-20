using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();

            IIoCManager ioCManager = new IoCManagerNinject();
            ioCManager.Configure();
            Application["ManagerIoC"] = ioCManager;

            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionManager.TouchSession(Context);
        }

        protected void Session_End(object sender, EventArgs E)
        {
            HttpContext.Current.Session.Abandon();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            IoCManagerNinject.kernel.Dispose();
        }
    }
}