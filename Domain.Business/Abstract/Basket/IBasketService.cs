using Domain.Entity.Container.Helper.Basket;
using Domain.Entity.Container.Request.Basket;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Basket
{
    public interface IBasketService
    {
        List<BasketItem> GetItemsFromTheBasket(RequestUserBasket request);
        void AddItemToTheBasket(RequestUserBasket request);
        void UpdateItemInTheBasket(RequestUserBasket request);
        void DeleteBasketItem(RequestUserBasket request);
        void DeleteTheBasket(RequestUserBasket request);
    }
}
