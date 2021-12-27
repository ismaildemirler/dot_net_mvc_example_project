using System;

namespace Domain.Entity.Container.Helper.Basket
{
    public class BasketItem
    {
        public Guid RedisId { get; set; }
        public string RelatedEntityId { get; set; }
        public int RelatedEntityType { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public int Amount { get; set; }
    }
}
