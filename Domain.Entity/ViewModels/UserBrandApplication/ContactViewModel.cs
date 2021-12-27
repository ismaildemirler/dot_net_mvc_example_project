using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserBrandApplication
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid ContactId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public int CustomerId { get; set; }

        [Display(Name = "İletişim Açıklaması")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim Adresi")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim İli")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim İlçesi")]
        public int TownId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public byte StateId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Fax")]
        public string FaxNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim Ad Soyad")]
        public string NameSurname { get; set; }

        [Display(Name = "Semt")]
        public string District { get; set; }

        [Display(Name = "Posta Kodu")]
        public string PostalCode { get; set; }
    }
}