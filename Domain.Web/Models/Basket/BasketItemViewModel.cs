using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public Guid RedisId { get; set; }
        public string RelatedEntityId { get; set; }
        public int RelatedEntityType { get; set; }

        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }

        [Display(Name = "Fiyat")]
        public string Price { get; set; }

        [Display(Name = "Adet")]
        public int Amount { get; set; }

        [Display(Name = "Toplam Fiyat")]
        public decimal TotalPrice { get { return Convert.ToDecimal(Price) * Amount; } }
    }
}