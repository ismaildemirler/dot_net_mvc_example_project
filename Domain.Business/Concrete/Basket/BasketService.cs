using Domain.Business.Abstract.Basket;
using Domain.Entity.Container.Helper.Basket;
using Domain.Entity.Container.Request.Basket;
using Domain.Infrastructure.Redis;
using System;
using System.Collections.Generic;

namespace Domain.Business.Concrete.Basket
{
    public class BasketService : IBasketService
    {
        private readonly string _basketKey;

        public BasketService()
        {
            _basketKey = "basket";
        }

        public List<BasketItem> GetItemsFromTheBasket(RequestUserBasket request)
        {
            RedisCacheManager<BasketItem> redis = new RedisCacheManager<BasketItem>(GetUserBasketKey(request.UserId));
            var userBasket = redis.GetItems();
            
            if(userBasket != null)
                return userBasket;

            return new List<BasketItem>();
        }

        public void AddItemToTheBasket(RequestUserBasket request)
        {
            RedisCacheManager<BasketItem> redis = new RedisCacheManager<BasketItem>(GetUserBasketKey(request.UserId));
            redis.AddItem(request.BasketItem.RedisId.ToString(), request.BasketItem);
        }

        public void UpdateItemInTheBasket(RequestUserBasket request)
        {
            RedisCacheManager<BasketItem> redis = new RedisCacheManager<BasketItem>(GetUserBasketKey(request.UserId));
            var oldBasketItem = redis.GetItem(request.BasketItem.RedisId.ToString());
            oldBasketItem.Amount = request.BasketItem.Amount;
            redis.UpdateItem(request.BasketItem.RedisId.ToString(), oldBasketItem);
        }

        public void DeleteBasketItem(RequestUserBasket request)
        {
            RedisCacheManager<BasketItem> redis = new RedisCacheManager<BasketItem>(GetUserBasketKey(request.UserId));
            redis.DeleteItem(request.BasketItem.RedisId.ToString());
        }

        public void DeleteTheBasket(RequestUserBasket request)
        {
            RedisCacheManager<BasketItem> redis = new RedisCacheManager<BasketItem>(GetUserBasketKey(request.UserId));
            redis.DeleteAllItems();
        }

        private string GetUserBasketKey(string userId)
        {
            return String.Format("{0}:{1}", _basketKey, userId);
        }
    }
}
