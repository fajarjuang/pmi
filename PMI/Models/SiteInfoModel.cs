using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMI.Resources.Model;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(SiteInfoMetadata))]
    public partial class SiteInfo
    {
    }

    internal class SiteInfoMetadata
    {
        [Required(ErrorMessageResourceName = "ThemeError", ErrorMessageResourceType = typeof(SiteInfoResources))]
        [Display(Name = "Theme", ResourceType = typeof(SiteInfoResources))]
        public string theme { get; set; }

        [AllowHtml]
        [Required(ErrorMessageResourceName = "FooterError", ErrorMessageResourceType = typeof(SiteInfoResources))]
        [Display(Name = "Footer", ResourceType = typeof(SiteInfoResources))]
        public string footer { get; set; }
    }
}