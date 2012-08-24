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

        private const int CATEGORY_PER_PAGE = 5;

        public ActionResult Index()
        {
            var posts = from p in db.Posts.Include(p => p.Category1)
                        group p by p.Category1 into cat
                        select cat.OrderByDescending(p => p.created).Take(CATEGORY_PER_PAGE);
            return View(posts.SelectMany(p => p));
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
