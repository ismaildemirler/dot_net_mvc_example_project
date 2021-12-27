using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.User.Models.Patent
{
    public class PatentInfoViewModel
    {
        public Guid PatentApplicationDetailId { get; set; }
        
        [Display(Name = "Buluş Başlığu")]
        public string Title { get; set; }

        [Display(Name = "Özet")]
        public string Summary { get; set; }
    }
}