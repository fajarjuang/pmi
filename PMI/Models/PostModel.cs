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
using PMI.Application.Helper;
using PMI.Application.Utils;
using PMI.Resources.Model;

namespace PMI.Models
{
    [MetadataTypeAttribute(typeof(PostMetadata))]
    public partial class Post : IValidatableObject
    {
        private const string UPLOAD_PATH = "~/Images/Uploads/Post";

        public string culturedTitle
        {
            private set { this.culturedTitle = value; }
            get
            {
                var culture = CultureHelper.GetCurrentNeutralCulture();
                var culturedTitle = "";

                if (culture == "id")
                {
                    culturedTitle = title;
                }
                else
                {
                    culturedTitle = string.IsNullOrEmpty(englishTitle) ? title : englishTitle;
                }

                return culturedTitle;
            }
        }

        public string culturedContent
        {
            private set { this.culturedContent = value; }
            get
            {
                var culture = CultureHelper.GetCurrentNeutralCulture();
                var culturedContent = "";

                if (culture == "id")
                {
                    culturedContent = content;
                }
                else
                {
                    culturedContent = string.IsNullOrEmpty(englishContent) ? content : englishContent;
                }

                return culturedContent;
            }
        
        }

        public string getContentSummary()
        {
            HtmlDocument doc = new HtmlDocument();

            try
            {
                doc.LoadHtml(this.culturedContent);
                HtmlNode firstParagraph = doc.DocumentNode.SelectNodes("//p[1]").First();
                return firstParagraph.InnerHtml;
            }
            catch (Exception)
            {
                return this.culturedContent;
            }
        }

        public void SaveImage(HttpPostedFileBase image)
        {
            if (image == null)
                return;

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
                    yield return new ValidationResult(PostModelResources.WriterError, new[] { "Title" });
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
        [Required(ErrorMessageResourceName = "TitleError", ErrorMessageResourceType = typeof(PostModelResources))]
        [Display(Name = "Title", ResourceType = typeof(PostModelResources))]
        public string title { get; set; }

        [StringLength(255)]
        [Display(Name = "EnglishTitle", ResourceType = typeof(PostModelResources))]
        public string englishTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime created { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public System.DateTime updated { get; set; }

        [Required(ErrorMessageResourceName = "CategoryError", ErrorMessageResourceType = typeof(PostModelResources))]
        [Display(Name = "Category", ResourceType = typeof(PostModelResources))]
        public long category { get; set; }

        [AllowHtml]
        [Required(ErrorMessageResourceName = "ContentError", ErrorMessageResourceType = typeof(PostModelResources))]
        [Display(Name = "Content", ResourceType = typeof(PostModelResources))]
        public string content { get; set; }

        [AllowHtml]
        [Display(Name = "EnglishContent", ResourceType = typeof(PostModelResources))]
        public string englishContent { get; set; }
    }
}