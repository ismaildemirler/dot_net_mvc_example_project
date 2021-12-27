using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserPatentApplication
{
    public class PatentApplicationDetailViewModel
    {
        [Required]
        public Guid PatentApplicationDetailId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Buluş Başlığı")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Buluş Özeti")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Buluşu zorunlu kılan sebepler")]
        public string PresentFeatures { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Buluşun getirdiği yenilikler")]
        public string PlannedFeatures { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid CustomerApplicationId { get; set; }

        [Display(Name = "Teknik çizimi mail atacağım")]
        public bool Mail { get; set; }
    }
}