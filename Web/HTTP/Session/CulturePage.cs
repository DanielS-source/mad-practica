using Es.Udc.DotNet.PracticaMaD.Web.Http.View;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.Session
{
    public class CulturePage : Page
    {
        protected override void InitializeCulture()
        {
            Locale locale = SessionManager.GetLocale(Context);

            Thread.CurrentThread.CurrentCulture = locale.Culture;
            Thread.CurrentThread.CurrentUICulture = locale.Culture;
        }

        protected bool IsValidGroup(string validationGroup)
        {
            foreach (BaseValidator validator in Page.Validators)
            {
                if (validator.ValidationGroup.Equals(validationGroup))
                {
                    bool valid = validator.IsValid;
                    if (valid)
                    {
                        validator.Validate();
                        valid = validator.IsValid;
                        validator.IsValid = true;
                    }
                    if (!valid)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}