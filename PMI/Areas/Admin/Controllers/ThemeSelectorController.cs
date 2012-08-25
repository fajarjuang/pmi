using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using PMI.Areas.Admin.Models;

namespace PMI.Areas.Admin.Controllers
{
    public class ThemeSelectorController : Controller
    {
        private string themeDir = WebConfigurationManager.AppSettings["ThemeDir"];
        //
        // GET: /Admin/ThemeSelector/

        public ActionResult Index()
        {
            var themeList = Directory.GetDirectories(Server.MapPath(themeDir));
            for (int i = 0; i < themeList.Length; i++)
            {
                themeList[i] = new DirectoryInfo(themeList[i]).Name;
            }

            return View(new ThemeModel { theme = themeList });
        }

        [HttpPost]
        public ActionResult Index(ThemeModel themeModel)
        {
            WebConfigurationManager.AppSettings["webpages:Theme"] = themeModel.theme[0];
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
