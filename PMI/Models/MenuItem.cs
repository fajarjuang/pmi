using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            mainMenu.Add(new MenuItem { Text = "Berita", Controller = "Home", Action = "Index", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = "Partisipasi", Controller = "Home", Action = "About", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = "Program", Controller = "Home", Action = "About", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = "Informasi", Controller = "Home", Action = "About", Active = false, Area = "" });
            mainMenu.Add(new MenuItem { Text = "Komunitas", Controller = "Home", Action = "About", Active = false, Area = "" });

            return mainMenu;
        }

        public static List<MenuItem> AdminMenu()
        {
            var adminMenu = new List<MenuItem>();
            adminMenu.Add(new MenuItem { Text = "Tulisan", Controller = "Post", Action = "Index", Active = false, Area = "Admin" });
            adminMenu.Add(new MenuItem { Text = "Kategori", Controller = "Category", Action = "Index", Active = false, Area = "Admin" });

            return adminMenu;
        }
    }
}