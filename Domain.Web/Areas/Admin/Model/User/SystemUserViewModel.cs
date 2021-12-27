using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.Admin.Model.User
{
    public class SystemUserViewModel
    {
        public string SystemUserId { get; set; }

        [Display(Name = "Adı")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadı")]
        public string LastName { get; set; }

        [Display(Name = "Adı Soyadı")]
        public string UserName { get { return FirstName + " " + LastName; } }

        [Display(Name = "E-Posta")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Durumu")]
        public string State { get; set; }

        [Display(Name = "Kullanıcı Tipi")]
        public string UserType { get; set; }

        [Display(Name = "İşlemler")]
        public string Buttons { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Sisteme Son Giriş Zamanı")]
        public DateTime? LastLoginDate { get; set; }
    }
}