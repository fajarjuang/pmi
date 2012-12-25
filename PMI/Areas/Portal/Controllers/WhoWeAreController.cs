using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PMI.Application.Mvc.Controller;

namespace PMI.Areas.Portal.Controllers
{
    public class WhoWeAreController : PMIController
    {
        //
        // GET: /Information/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisionAndMission()
        {
            return View();
        }

    }
}
