using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMI.Application.Mvc.Controller;
using PMI.Application.Utils;
using PMI.Models;
using PMI.Resources.Global;

namespace PMI.Controllers
{
    [ChildActionOnly]
    public class NavigationController : PMIController
    {
        private pmiEntities db = new pmiEntities();

        private List<MenuItem> PortalMainMenu = MenuItem.PortalMainMenu();
        private List<MenuItem> NewsMainMenu = MenuItem.NewsMainMenu();
        private List<MenuItem> AdminMenu = MenuItem.AdminMenu();

        public PartialViewResult Menu(string controller, string action)
        {
            var menu = NewsMainMenu;
            // -- Login menu disabled. Uncomment to enable.
            menu.Add(AuthMenu()); 
            if (User.IsInRole("CanPostNews"))
                menu.AddRange(AdminMenu);

            menu.Where(p => p.Action == action && p.Controller == controller)
                .Select(p => { p.Active = true; return p; })
                .ToList(); // to activate the lazy evaluation
            return PartialView(menu);
        }

        public PartialViewResult MainMenu(String controller, string action)
        {
            var menu = PortalMainMenu;

            menu.Where(p => p.Action == action && p.Controller == controller)
                .Select(p => { p.Active = true; return p; })
                .ToList(); // lazy eval

            return PartialView(menu);
        }

        public PartialViewResult Footer(string controller, string action)
        {
            var footer = db.SiteInfoes.FirstOrDefault().footer;
            ViewBag.footer = HtmlUtils.SanitizeHtml(footer); // because you can't call extension method from partial view

            return PartialView();
        }

        private MenuItem AuthMenu()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new MenuItem { Text = GlobalResources.MenuLogout, Controller = "Account", Action = "LogOff", Area = "", Active = false };
            }
            else
            {
                return new MenuItem { Text = GlobalResources.MenuLogin, Controller = "Account", Action = "LogOn", Area = "", Active = false };
            }
        }
    }
}
