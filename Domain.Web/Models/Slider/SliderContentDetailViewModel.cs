using Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Web.Models.Slider
{
    public class SliderContentDetailViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid SliderContentDetailId { get; set; }
        
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public string SliderContentId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Metin")]
        public string ContentInfo { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Sıra")]
        public byte RankNumber { get; set; }

        [Display(Name = "İşlemler")]
        public string Buttons { get; set; }
    }
}