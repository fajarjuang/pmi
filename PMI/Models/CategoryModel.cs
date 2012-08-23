using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using PMI.Utils;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    internal class CategoryMetadata
    {
        [StringLength(255)]
        [DisplayName("Kategori")]
        public string desc { get; set; }
    }
}