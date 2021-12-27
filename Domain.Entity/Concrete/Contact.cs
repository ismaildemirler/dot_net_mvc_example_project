using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("Contact")]
    public class Contact : IEntity
    {
        [Key]
        [Required]
        public Guid ContactId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "İletişim Açıklaması")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "İletişim Adresi")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "İletişim İli")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "İletişim İlçesi")]
        public int TownId { get; set; }

        [Required]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required]
        public byte StateId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        
        [Display(Name = "Fax")]
        public string FaxNumber { get; set; }
        
        [Display(Name = "İletişim Ad Soyad")]
        public string NameSurname { get; set; }

        [Display(Name = "Semt")]
        public string District { get; set; }

        [Display(Name = "Posta Kodu")]
        public string PostalCode { get; set; }
    }
}
