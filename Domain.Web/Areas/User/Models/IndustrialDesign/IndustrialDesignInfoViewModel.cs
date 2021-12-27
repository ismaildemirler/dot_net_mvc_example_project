using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.User.Models.IndustrialDesign
{
    public class IndustrialDesignInfoViewModel
    {
        public Guid IndustrialDesignApplicationDetailId { get; set; }
        
        [Display(Name = "Tasarım Başlığı")]
        public string Title { get; set; }

        [Display(Name = "Fotoğraf")]
        public string Image { get; set; }
    }
}