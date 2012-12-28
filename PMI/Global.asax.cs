using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using PMI.Application.Mvc;
using PMI.Models;

namespace PMI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        public static void RegisterViewEngine(ViewEngineCollection viewEngines)
        {
            viewEngines.Clear();

            var PMIViewEngine = new PMIViewEngine();

            viewEngines.Add(PMIViewEngine);
        }

        protected void Application_Start()
        {
            if (!Roles.RoleExists("CanPostNews"))
                Roles.CreateRole("CanPostNews");

            if (Membership.GetUser("admin") == null)
            {
                Membership.CreateUser("admin", "admin@pmi.or.id", "admin@pmi.or.id");
                Roles.AddUserToRole("admin", "CanPostNews");
            }

            var db = new pmiEntities();
            var si = db.SiteInfoes.Find(1);

            if (si == null)
            {
                var defaultSiteInfo = new SiteInfo();
                defaultSiteInfo.theme = "Default";
                defaultSiteInfo.footer = "footer";
                db.SiteInfoes.Add(defaultSiteInfo);
                db.SaveChanges();
            }

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterViewEngine(ViewEngines.Engines);
        }
    }
}