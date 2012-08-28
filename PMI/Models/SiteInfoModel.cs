using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(SiteInfoMetadata))]
    public partial class SiteInfo
    {
    }

    internal class SiteInfoMetadata
    {
        [DisplayName("Tema")]
        public string theme { get; set; }

        [AllowHtml]
        [DisplayName("Footer Situs")]
        public string footer { get; set; }
    }
}