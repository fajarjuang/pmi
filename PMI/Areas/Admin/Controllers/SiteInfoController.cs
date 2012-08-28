﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using PMI.Models;

namespace PMI.Areas.Admin.Controllers
{ 
    public class SiteInfoController : Controller
    {
        private pmiEntities db = new pmiEntities();

        private SelectList getThemeList(string selectedValue)
        {
            var themeDir = WebConfigurationManager.AppSettings["ThemeDir"];
            var themeList = Directory.GetDirectories(Server.MapPath(themeDir));

            var themes = from string t in themeList
                         let theme = new DirectoryInfo(t).Name
                         select new { id = theme, name = theme };

            return new SelectList(themes, "id", "name", selectedValue);
        }

        //
        // GET: /Admin/SiteInfo/
        public ActionResult Index()
        {
            SiteInfo siteinfo = db.SiteInfoes.FirstOrDefault();
            ViewBag.theme = getThemeList(siteinfo.theme);
            return View(siteinfo);
        }

        //
        // POST: /Admin/SiteInfo/
        [HttpPost]
        public ActionResult Index(SiteInfo siteinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteinfo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}