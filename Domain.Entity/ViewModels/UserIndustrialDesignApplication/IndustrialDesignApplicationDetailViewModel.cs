using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserIndustrialDesignApplication
{
    public class IndustrialDesignApplicationDetailViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid IndustrialDesignApplicationDetailId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Tasarım Başlığı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid CustomerApplicationId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Tasarım Başvuru Tarihi")]
        public DateTime DesignApplicationDate { get; set; }
    }
}