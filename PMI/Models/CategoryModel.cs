using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using PMI.Application.Helper;
using PMI.Application.Utils;
using PMI.Resources.Model;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(CategoryMetadata))]
    public partial class Category
    {
        public string culturedDesc
        {
            private set { this.culturedDesc = value; }

            get
            {
                var culture = CultureHelper.GetCurrentNeutralCulture();
                var culturedDesc = "";
                if (culture == "id")
                {
                    culturedDesc = this.desc;
                }
                else
                {
                    culturedDesc = this.englishDesc;
                }

                return culturedDesc;
            }
        }
    }

    internal class CategoryMetadata
    {
        [StringLength(255)]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(GlobalModelResources))]
        [Display(Name = "Description", ResourceType = typeof(CategoryModelResources))]
        public string desc { get; set; }

        [StringLength(255)]
        [Display(Name = "EnglishDescription", ResourceType = typeof(CategoryModelResources))]
        public string englishDesc { get; set; }
    }
}