using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMI.Models;

namespace PMI.Controllers
{
    [ChildActionOnly]
    public class NavigationController : Controller
    {
        private List<MenuItem> MainMenu = MenuItem.MainMenu();
        private List<MenuItem> AdminMenu = MenuItem.AdminMenu();

        public PartialViewResult Menu(string controller, string action)
        {
            var menu = MainMenu;
            menu.Add(AuthMenu());
            if (User.IsInRole("CanPostNews"))
                menu.AddRange(AdminMenu);

            menu.Where(p => p.Action == action && p.Controller == controller)
                .Select(p => { p.Active = true; return p; })
                .ToList(); // to activate the lazy evaluation
            return PartialView(menu);
        }

        private MenuItem AuthMenu()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new MenuItem { Text = "Logout", Controller = "Account", Action = "LogOff", Area = "", Active = false };
            }
            else
            {
                return new MenuItem { Text = "Login", Controller = "Account", Action = "LogOn", Area = "", Active = false };
            }
        }
    }
}
