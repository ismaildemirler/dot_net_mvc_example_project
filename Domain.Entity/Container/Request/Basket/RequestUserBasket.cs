using Domain.Entity.Container.Helper.Basket;
using System;

namespace Domain.Entity.Container.Request.Basket
{
    public class RequestUserBasket
    {   
        public string UserId { get; set; }
        public BasketItem BasketItem { get; set; }
    }
}
