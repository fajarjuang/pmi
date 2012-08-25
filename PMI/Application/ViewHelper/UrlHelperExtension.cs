using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PMI.Application.ViewHelper
{
    public static class UrlHelperExtension
    {
        public static string ThemeContent(this UrlHelper url, string path)
        {
            var theme = WebConfigurationManager.AppSettings["webpages:Theme"];
            var finalPath = "~/Themes/";

            if (string.IsNullOrEmpty(theme))
                finalPath = finalPath + "Default/";
            else
                finalPath = finalPath + theme + "/";

            // path cleaning
            if (path.StartsWith("~/"))
                path = path.Remove(0, 2);

            if (path.StartsWith("/"))
                path = path.Remove(0, 1);

            path = path.Replace("../", string.Empty);
            finalPath = finalPath + path;

            return VirtualPathUtility.ToAbsolute(finalPath);
        }
    }
}