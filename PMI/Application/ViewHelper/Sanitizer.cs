using System.Web.Mvc;
using PMI.Application.Utils;

namespace PMI.Application.ViewHelper
{
    public static class Sanitizer
    {
        public static MvcHtmlString SanitizeHTML(this HtmlHelper helper, string html)
        {
            return new MvcHtmlString(HtmlUtility.SanitizeHtml(html));
        }
    }
}