using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using PMI.Application.Mvc.Controller;
using PMI.Application.Utils;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(PostMetadata))]
    public partial class Post : IValidatableObject
    {
        private const string UPLOAD_PATH = "~/Images/Uploads/Post";

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

        public void SaveImage(HttpPostedFileBase image)
        {
            var path = "";
            if (image.ContentLength > 0)
            {
                var filename = TextUtils.MD5(title) + Path.GetFileName(image.FileName);
                var savePath = HttpContext.Current.Server.MapPath(UPLOAD_PATH);
                CreateDirectory(savePath);
                
                path = Path.Combine(savePath, filename);
                image.SaveAs(path);
                this.image = UPLOAD_PATH + "/" + filename;
            }
        }

        // This method will be used only here, while creating the directory.
        // We don't want it to be too complicated so let's do a YAGNI here
        // and forget about the Single Responsibility Principle.
        // Please remember to throw this one out to a new class if you ever
        // need more than one FileSystem IO functionality one day.
        private void CreateDirectory(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new pmiEntities();
            var currentPost = db.Posts.Where(p => p.id == this.id).FirstOrDefault();

            if (currentPost != null)
            {
                this.updated = DateTime.Now;

                if (this.writer != currentPost.writer)
                    yield return new ValidationResult("Penulis tidak sama dengan penulis awal.", new[] { "Title" });
            }
            else
            {
                this.created = DateTime.Now;
                this.updated = DateTime.Now;
            }
        }
    }

    internal class PostMetadata
    {
        [StringLength(255)]
        [Required(ErrorMessage = "Judul tulisan harus diisi.")]
        [DisplayName("Judul Tulisan")]
        public string title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime created { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime updated { get; set; }

        [Required(ErrorMessage = "Kategori harus dipilih.")]
        [DisplayName("Kategori")]
        public long category { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Isi tulisan tidak boleh kosong.")]
        [DisplayName("Isi")]
        public string content { get; set; }
    }
}