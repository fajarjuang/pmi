using System.Web.Mvc;
using PMI.Utils;

namespace PMI.ViewHelper
{
    public static class Sanitizer
    {
        public static MvcHtmlString SanitizeHTML(this HtmlHelper helper, string html)
        {
            return new MvcHtmlString(HtmlUtility.SanitizeHtml(html));
        }
    }
}