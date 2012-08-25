using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace PMI.Areas.Admin.Models
{
    public class ThemeModel
    {
        [DisplayName("Tema")]
        public string[] theme { get; set; }
    }
}