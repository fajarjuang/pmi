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
        [Required(ErrorMessage = "Tema harus dipilih!")]
        [DisplayName("Tema")]
        public string theme { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Footer harus diisi!")]
        [DisplayName("Footer Situs")]
        public string footer { get; set; }
    }
}