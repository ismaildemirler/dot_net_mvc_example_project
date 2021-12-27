using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserBrandApplication
{
    public class ContactOtherViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid ContactId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public int CustomerId { get; set; }

        [Display(Name = "İletişim Açıklaması")]
        [StringLength(100, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim Adresi")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim İli")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim İlçesi")]
        public int TownId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Telefon Numarası")]
        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public byte StateId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Fax")]
        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string FaxNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İletişim Ad Soyad")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Semt")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string District { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Posta Kodu")]
        [StringLength(10, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string PostalCode { get; set; }
    }
}