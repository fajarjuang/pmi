using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public static List<MenuItem> MainMenu()
        {
            var mainMenu = new List<MenuItem>();
            mainMenu.Add(new MenuItem { Text = GlobalResources.MenuBerita, Controller = "Home", Action = "Index", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = GlobalResources.MenuHeadline, Controller = "Home", Action = "Headline", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = GlobalResources.MenuEvent, Controller = "Home", Action = "Events", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = GlobalResources.MenuPR, Controller = "Home", Action = "PressRelease", Active = false, Area = "" });

            return mainMenu;
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