using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Web.Models.Log
{
    public class ErrorLogViewModel
    {
        [Display(Name = "ID")]
        public long ElasticId { get; set; }

        [Display(Name = "Hata Metot Adı")]
        public string MethodName { get; set; }

        [Display(Name = "Hata Mesajı")]
        public string ErrorMessage { get; set; }

        [Display(Name = "Hata Detayı")]
        public string ErrorDetail { get; set; }

        [Display(Name = "Kullanıcı ID")]
        public int UserId { get; set; }

        [Display(Name = "Kullanıcı IP")]
        public string Ip { get; set; }

        [Display(Name = "Sunucu Adı")]
        public string ServerName { get; set; }
        
        [Display(Name = "Hata Alınan Tarih")]
        public DateTime ErrorTime { get; set; }
    }
}