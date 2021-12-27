using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Model.Parameter
{
    public class DomainPriceViewModel
    {
        public string DomainPriceId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Domain Uzantısı")]
        public int TLDTypeId { get; set; }

        [Display(Name = "Domain Uzantısı")]
        public string TLDTypeDescription { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Range(int.MinValue, int.MaxValue)]
        [Display(Name = "Kayıt Fiyatı")]
        public decimal RegisterPrice { get; set; }

        [Range(int.MinValue, int.MaxValue)]
        [Display(Name = "Kayıt Fiyatı (İndirimli)")]
        public decimal? RegisterPriceWithDiscount { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Range(int.MinValue, int.MaxValue)]
        [Display(Name = "Tranfer Fiyatı")]
        public decimal TransferPrice { get; set; }

        [Range(int.MinValue, int.MaxValue)]
        [Display(Name = "Tranfer Fiyatı (İndirimli)")]
        public decimal? TransferPriceWithDiscount { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Range(int.MinValue, int.MaxValue)]
        [Display(Name = "Yenileme Uzantısı")]
        public decimal RefreshPrice { get; set; }
        
        [Range(int.MinValue, int.MaxValue)]
        [Display(Name = "Yenileme Fiyatı (İndirimli)")]
        public decimal? RefreshPriceWithDiscount { get; set; }

        [Display(Name = "İşlemler")]
        public string Buttons { get; set; }

        public IEnumerable<SelectListItem> TLDTypes { get; set; }
    }
}