using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PMI.Models;

namespace PMI.Controllers
{
    public class HomeController : Controller
    {
        private pmiEntities db = new pmiEntities();

        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category1);
            return View(posts);
        }

        public ActionResult News(long id)
        {
            var post = db.Posts.Find(id);
            return View(post);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
