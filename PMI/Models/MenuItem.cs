using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMI.Resources;
using PMI.Resources.Global;

namespace PMI.Models
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public bool Active { get; set; }

        public static List<MenuItem> PortalMainMenu()
        {
            var portalMainMenu = new List<MenuItem>();
            portalMainMenu.Add(new MenuItem { Text = MainPageResources.HomeMenu, Controller = "Home", Action = "Index", Active = false, Area = "" });
            portalMainMenu.Add(new MenuItem { Text = MainPageResources.NewsMenu, Controller = "Home", Action = "AllNews", Active = false, Area = "" });
            portalMainMenu.Add(new MenuItem { Text = MainPageResources.WhoWeAreMenu, Controller = "WhoWeAre", Action = "Index", Active = false, Area = "Portal" });
            portalMainMenu.Add(new MenuItem { Text = MainPageResources.WhatWeDoMenu, Controller = "Programs", Action = "Index", Active = false, Area = "Portal" });
            portalMainMenu.Add(new MenuItem { Text = MainPageResources.ContactUsMenu, Controller = "Community", Action = "Index", Active = false, Area = "Portal" });
            portalMainMenu.Add(new MenuItem { Text = MainPageResources.PublicMemberMenu, Controller = "Account", Action = "Logon", Active = false, Area = "" });

            return portalMainMenu;
        }

        public static List<MenuItem> NewsMainMenu()
        {
            var newsMainMenu = new List<MenuItem>();
            newsMainMenu.Add(new MenuItem { Text = GlobalResources.MenuBerita, Controller = "Home", Action = "AllNews", Active = false, Area = "" });
            newsMainMenu.Add(new MenuItem { Text = GlobalResources.MenuHeadline, Controller = "Home", Action = "Headline", Active = false, Area = "" });
            newsMainMenu.Add(new MenuItem { Text = GlobalResources.MenuEvent, Controller = "Home", Action = "Events", Active = false, Area = "" });
            newsMainMenu.Add(new MenuItem { Text = GlobalResources.MenuPR, Controller = "Home", Action = "PressRelease", Active = false, Area = "" });

            return newsMainMenu;
        }

        public static List<MenuItem> AdminMenu()
        {
            var adminMenu = new List<MenuItem>();
            adminMenu.Add(new MenuItem { Text = GlobalResources.MenuTulisan, Controller = "Post", Action = "Index", Active = false, Area = "Admin" });
            adminMenu.Add(new MenuItem { Text = GlobalResources.MenuKategori, Controller = "Category", Action = "Index", Active = false, Area = "Admin" });
            adminMenu.Add(new MenuItem { Text = GlobalResources.MenuPengaturan, Controller = "SiteInfo", Action = "Index", Active = false, Area = "Admin" });

            return adminMenu;
        }
    }
}