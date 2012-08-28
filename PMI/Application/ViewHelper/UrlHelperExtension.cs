using System.Web;
using System.Web.Mvc;
using PMI.Models;

namespace PMI.Application.ViewHelper
{
    public static class UrlHelperExtension
    {
        public static string ThemeContent(this UrlHelper url, string path)
        {
            var db = new pmiEntities();
            var theme = db.SiteInfoes.Find(1).theme; // there wouldn't be any other key than 1. If there is, something's wrong!

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