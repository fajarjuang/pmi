using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PMI.Application.Helper;

namespace PMI.Application.Mvc.Controller
{
    public class PMIController : System.Web.Mvc.Controller
    {
        protected override void ExecuteCore()
        {
            string culture = null;
            HttpCookie currentCulture = Request.Cookies["_culture"];

            if (currentCulture == null)
                culture = Request.UserLanguages[0];
            else
                culture = currentCulture.Value;

            culture = CultureHelper.GetImplementedCulture(culture);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.ExecuteCore();
        }
    }
}