using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PMI.Application.Mvc.Controller;
using PMI.Models;
using PagedList;

namespace PMI.Controllers
{
    public class HomeController : PMIController
    {
        private pmiEntities db = new pmiEntities();

        private const int CATEGORY_PER_PAGE = 5;
        private const int NEWS_PER_PAGE = 15;

        public ActionResult Index()
        {
            var categories = from c in db.Categories
                             select c;

            ViewBag.Headline = (from cat in categories
                                where cat.englishDesc == "Headline News"
                                select cat).FirstOrDefault().culturedDesc;
            ViewBag.Event = (from cat in categories
                             where cat.englishDesc == "Events"
                             select cat).FirstOrDefault().culturedDesc;
            ViewBag.PressRelease = (from cat in categories
                                    where cat.englishDesc == "Press Release"
                                    select cat).FirstOrDefault().culturedDesc;

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

        public ActionResult NewsList(int? page)
        {
            var pageNumber = page ?? 1;
            var posts = from p in db.Posts
                        orderby p.created descending
                        select p;

            return View(posts.ToPagedList(pageNumber, NEWS_PER_PAGE));
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
