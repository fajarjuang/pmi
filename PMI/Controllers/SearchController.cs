using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PMI.Models;

namespace PMI.Controllers
{
    public class SearchController : Controller
    {
        private pmiEntities db = new pmiEntities();

        private const int RESULT_PER_PAGE = 10;

        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Search/Result?query=search+query&page=1

        public ActionResult Result(string query, int? page)
        {
            var searchResult = from p in db.Posts
                               where p.title.Contains(query) || p.englishTitle.Contains(query)
                               orderby p.created descending
                               select p;

            var pageNumber = page ?? 1;

            return View(searchResult.ToPagedList(pageNumber, RESULT_PER_PAGE));
        }

    }
}
