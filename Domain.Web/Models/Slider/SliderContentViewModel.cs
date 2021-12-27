using Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Web.Models.Slider
{
    public class SliderContentViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public string SliderContentId { get; set; }

        public byte[] SliderImage { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(100, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Başlık")]
        public string Header { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(200, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Konu")]
        public string Topic { get; set; }

        [Display(Name = "Fiyat Bölümü Olacak Mı?")]
        public bool HasPriceSection { get; set; }

        [Display(Name = "Fiyat Bölümü Olacak Mı?")]
        public string HasPriceSectionDesc { get; set; }

        [Display(Name = "Fiyat")]
        public decimal? Price { get; set; }

        [Display(Name = "Fiyat Tipi")]
        public EnumCurrencyType? CurrencyTypeId { get; set; }

        [Display(Name = "Fiyat Süresi")]
        public EnumDurationType? DurationTypeId { get; set; }

        [StringLength(200, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Fiyat Açıklama")]
        public string PriceDescription { get; set; }

        [Display(Name = "Detay Butonu Olacak Mı?")]
        public bool HasDetailLink { get; set; }

        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Detay Adresi")]
        public string DetailUrl { get; set; }

        [Display(Name = "Durum")]
        public string StateDesc { get; set; }

        [Display(Name = "İşlemler")]
        public string Buttons { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }

        public HttpPostedFileBase FileSlider { get; set; }

        public bool ImageDeleted { get; set; }
    }
}