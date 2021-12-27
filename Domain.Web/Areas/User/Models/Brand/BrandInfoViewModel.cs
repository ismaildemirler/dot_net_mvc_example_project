using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.User.Models.Brand
{
    public class BrandInfoViewModel
    {
        public int UserId { get; set; }
        
        [Display(Name = "Marka Adı")]
        public string BrandName { get; set; }

        [Display(Name = "Marka Başvuru Şekli")]
        public byte BrandApplicationTypeId { get; set; }

        public string SpecialClass { get; set; }
        public Guid ContactId { get; set; }
    }
}