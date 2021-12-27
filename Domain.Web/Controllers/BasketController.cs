using Domain.Business.Abstract.Basket;
using Domain.Entity.Container.Helper.Basket;
using Domain.Entity.Container.Request.Basket;
using Domain.Entity.Enum;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Models.Basket;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _BasketItems()
        {
            List<BasketItemViewModel> list = new List<BasketItemViewModel>();
            var response = _basketService.GetItemsFromTheBasket(GetRequestUserBasketWithUserId());
            if (response != null)
            {
                list = response.Select(s=> new BasketItemViewModel() { 
                    RedisId = s.RedisId,
                    RelatedEntityId = s.RelatedEntityId,
                    RelatedEntityType = s.RelatedEntityType,
                    ProductName = s.ProductName,
                    Amount = s.Amount,
                    Price = s.Price
                }).ToList();
            }
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult GetBasketItems()
        {
            List<BasketItemViewModel> list = new List<BasketItemViewModel>();
            var response = _basketService.GetItemsFromTheBasket(GetRequestUserBasketWithUserId());
            if (response != null)
            {
                list = response.Select(s => new BasketItemViewModel()
                {
                    RedisId = s.RedisId,
                    RelatedEntityId = s.RelatedEntityId,
                    RelatedEntityType = s.RelatedEntityType,
                    ProductName = s.ProductName,
                    Amount = s.Amount,
                    Price = s.Price
                }).ToList();
            }
            return Json(list);
        }

        [HttpPost]
        public ActionResult AddItemToTheBasket(BasketItem basketItem)
        {
            var request = GetRequestUserBasketWithUserId();
            basketItem.RedisId = Guid.NewGuid();
            request.BasketItem = basketItem;
            _basketService.AddItemToTheBasket(request);
            return ShowMessageJSON("Ürün sepete eklendi.", WebHelpers.Models.Shared.AjaxResultState.Success);
        }

        [HttpPost]
        public ActionResult UpdateItemAmountInBasket(Guid id, int amount)
        {
            var request = new RequestUserBasket()
            {
                UserId = CurrentUser.UserLoginGuid,
                BasketItem = new BasketItem()
                {
                    RedisId = id,
                    Amount = amount
                }
            };
            _basketService.UpdateItemInTheBasket(request);

            return ShowMessageJSON("Sepet güncellendi.", WebHelpers.Models.Shared.AjaxResultState.Success);
        }

        [HttpPost]
        public ActionResult RemoveItemFromTheBasket(Guid id)
        {
            var request = new RequestUserBasket()
            {
                UserId = CurrentUser.UserLoginGuid,
                BasketItem = new BasketItem() { 
                    RedisId = id
                }
            };
            _basketService.DeleteBasketItem(request);

            return ShowMessageJSON("Ürün sepetten çıkarıldı.", WebHelpers.Models.Shared.AjaxResultState.Success);
        }

        [HttpPost]
        public ActionResult DeleteTheBasket()
        {
            _basketService.DeleteTheBasket(GetRequestUserBasketWithUserId());

            return ShowMessageJSON("Sepetteki tüm ürünler kaldırıldı.", WebHelpers.Models.Shared.AjaxResultState.Success);
        }

        private RequestUserBasket GetRequestUserBasketWithUserId() { 
            return new RequestUserBasket()
            {
                UserId = CurrentUser.UserLoginGuid
            };
        }
    }
}