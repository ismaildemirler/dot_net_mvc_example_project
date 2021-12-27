using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Domain.Web.Models.Blog
{
    public class BlogContentViewModel
    {
        public Guid BlogContentId { get; set; }

        [Required]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "İçerik")]
        public string Contents { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "İçerik (Özet)")]
        public string ShortContent { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Display(Name = "Resim")]
        public HttpPostedFileBase PostImage { get; set; }

        public byte[] PostImageByteArray { get; set; }

        public List<SelectListItem> BlogCategories { get; set; }
    }
}