using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using PMI.Application.Utils;
using PMI.Resources.Model;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    internal class CategoryMetadata
    {
        [StringLength(255)]
        [Required(ErrorMessageResourceName = "DescriptionError", ErrorMessageResourceType = typeof(CategoryModelResources))]
        [Display(Name = "Description", ResourceType = typeof(CategoryModelResources))]
        public string desc { get; set; }
    }
}