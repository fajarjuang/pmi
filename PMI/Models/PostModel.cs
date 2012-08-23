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
    [MetadataTypeAttribute(typeof(PostMetadata))]
    public partial class Post
    {
        public string getContentSummary()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(this.content);

            try
            {
                HtmlNode firstParagraph = doc.DocumentNode.SelectNodes("//p[1]").First();
                return firstParagraph.InnerHtml;
            }
            catch (Exception)
            {
                return this.content;
            }
        }
    }

    internal class PostMetadata
    {
        [StringLength(255)]
        [DisplayName("Judul Tulisan")]
        public string title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime created { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime updated { get; set; }

        [DisplayName("Kategori")]
        public long category { get; set; }

        [AllowHtml]
        [DisplayName("Isi")]
        public string content { get; set; }
    }
}