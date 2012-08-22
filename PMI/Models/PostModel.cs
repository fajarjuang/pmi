using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [MetadataTypeAttribute(typeof(PostMetadata))]
    public partial class Post
    {
    }

    internal class PostMetadata
    {
        [StringLength(255)]
        [DisplayName("Judul Tulisan")]
        public string title { get; set; }

        [DisplayFormat(DataFormatString="{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime created { get; set; }

        [DisplayFormat(DataFormatString="{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime updated { get; set; }

        [DisplayName("Kategori")]
        public long category { get; set; }

        [AllowHtml]
        [DisplayName("Isi")]
        public string content { get; set; }
    }
}