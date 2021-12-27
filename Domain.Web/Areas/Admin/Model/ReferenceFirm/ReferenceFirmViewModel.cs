using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Domain.Web.Areas.Admin.Models.ReferenceFirm
{
    public class ReferenceFirmViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public string ReferenceId { get; set; }

        public byte[] LogoImage { get; set; }

        [StringLength(350, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Yapılan İşin Başlığı")]
        public string WorkName { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(350, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Referans Firma Adı")]
        public string Name { get; set; }

        [Display(Name = "İşin Açıklaması")]
        public string Detail { get; set; }

        [Display(Name = "İşlemler")]
        public string Buttons { get; set; }

        public HttpPostedFileBase FileReference { get; set; }

        public DateTime InsertDate { get; set; }

        public string StateDesc { get; set; }

        public bool ImageDeleted { get; set; }
    }
}