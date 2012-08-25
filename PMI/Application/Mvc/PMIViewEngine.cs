using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PMI.Application.Mvc
{
    public class PMIViewEngine : IViewEngine
    {
        private RazorViewEngine fallbackViewEngine = new RazorViewEngine();

        public string theme { get; set; }

        private RazorViewEngine CreatePMIViewEngine()
        {
            var pmiViewEngine = new RazorViewEngine();
            this.theme = WebConfigurationManager.AppSettings["webpages:Theme"];

            pmiViewEngine.PartialViewLocationFormats = new []
                {
                    "~/Themes/" + theme + "/Views/{1}/{0}.cshtml",
                    "~/Themes/" + theme + "/Views/Shared/{0}.cshtml",
                    "~/Themes/" + theme + "/Views/Shared/{1}/{0}.cshtml"
                }.Union(pmiViewEngine.PartialViewLocationFormats).ToArray();

            pmiViewEngine.AreaPartialViewLocationFormats = new[]
                {
                    "~/Themes/" + theme + "/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Themes/" + theme + "/Areas/{2}/Views/Shared/{0}.cshtml",
                    "~/Themes/" + theme + "/Areas/{2}/Views/Shared/{1}/{0}.cshtml",
                }.Union(pmiViewEngine.AreaPartialViewLocationFormats).ToArray();

            pmiViewEngine.ViewLocationFormats = new []
                {
                    "~/Themes/" + theme + "/Views/{1}/{0}.cshtml"
                }.Union(pmiViewEngine.ViewLocationFormats).ToArray();

            pmiViewEngine.AreaViewLocationFormats = new[]
                {
                    "~/Themes/" + theme + "/Areas/{2}/Views/{1}/{0}.cshtml"
                }.Union(pmiViewEngine.AreaViewLocationFormats).ToArray();

            pmiViewEngine.MasterLocationFormats = new []
                {
                    "~/Themes/" + theme + "/Views/{1}/{0}.cshtml",
                    "~/Themes/" + theme + "/Views/Shared/{0}.cshtml",
                    "~/Themes/" + theme + "/Views/Shared/{1}/{0}.cshtml"
                }.Union(pmiViewEngine.MasterLocationFormats).ToArray();

            pmiViewEngine.AreaMasterLocationFormats = new[]
                {
                    "~/Themes/" + theme + "/Areas/{2}/Views/{1}/{0}.cshtml",
                    "~/Themes/" + theme + "/Areas/{2}/Views/Shared/{0}.cshtml",
                    "~/Themes/" + theme + "/Areas/{2}/Views/Shared/{1}/{0}.cshtml",
                }.Union(pmiViewEngine.AreaMasterLocationFormats).ToArray();

            return pmiViewEngine;
        }

        private RazorViewEngine CreateRealViewEngine()
        {
            try 
            {	        
                return CreatePMIViewEngine();
            }
            catch (Exception)
            {
                return fallbackViewEngine;
            }
        }

        public ViewEngineResult  FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return CreateRealViewEngine().FindPartialView(controllerContext, partialViewName, useCache);
        }

        public ViewEngineResult  FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return CreateRealViewEngine().FindView(controllerContext, viewName, masterName, useCache);
        }

        public void  ReleaseView(ControllerContext controllerContext, IView view)
        {
            CreateRealViewEngine().ReleaseView(controllerContext, view);
        }
    }
}