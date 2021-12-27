using Domain.Entity.Concrete;
using Domain.Entity.Enum;
using Domain.Infrastructure.CrossCuttingConcerns.Session;
using Domain.Infrastructure.WebServices;
using Domain.Web.Models.Domain;
using Domain.Web.Models.Domain.DomainApplication;
using Domain.WebHelpers.Infrastructre;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CheckAvailabilityRequest = Olipso.Api.ClientApi.OlipsoDomainApi.CheckAvailabilityRequest;
using ExecutionStatus = Olipso.Api.ClientApi.OlipsoDomainApi.ExecutionStatus;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class DomainController : BaseController
    {
        public DomainApplicationWrapperViewModel DomainApplicationWrapperViewModelSession
        {
            get
            {
                if (SessionManager.Get("DomainApplicationWrapperViewModelSession") == null)
                    SessionManager.Set("DomainApplicationWrapperViewModelSession", new DomainApplicationWrapperViewModel());
                return (DomainApplicationWrapperViewModel)SessionManager.Get("DomainApplicationWrapperViewModelSession");

            }
            set => SessionManager.Set("DomainApplicationWrapperViewModelSession", value);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchDomain(DomainSearchViewModel model)
        {
            var domanApi = WebServiceClient.DomainApiServiceClient;
            var namelist = new List<string>();
            foreach (var item in model.DomainName.Split(','))
            {
                namelist.Add(item.Split('.')[0]);
            }
            var response = domanApi.CheckAvailability(new CheckAvailabilityRequest()
            {
                Period = 1,
                Commad = "create",
                DomainNameList = namelist,
                TldList = "com,org,net,city,online,info,store".Split(',').ToList()
            });

            var domainApiInfoList = new DomainApiResponseInfoListViewModel();
            if (response.OperationResult == ExecutionStatus.SUCCESS)
            {
                foreach (var item in response.DomainAvailabilityInfoList)
                {
                    DomainPrice domainPriceModel = null;
                    var tldModel = CacheManager.GetAllTldType.FirstOrDefault(f => f.Description == item.Tld);
                    if (tldModel != null)
                    {
                        domainPriceModel = CacheManager.GetAllDomainPrices.FirstOrDefault(f => f.TLDTypeId == tldModel.TLDTypeId);
                    }

                    var domainApiInfoModel = new DomainApiResponseInfoViewModel
                    {
                        ClassKey = item.ClassKey,
                        Command = item.Command,
                        Currency = domainPriceModel?.Currency,
                        DomainName = item.DomainName,
                        IsFee = item.IsFee,
                        Period = item.Period,
                        Price = domainPriceModel?.RegisterPrice,
                        Reason = item.Reason,
                        Tld = item.Tld,
                        TldId = domainPriceModel != null ? domainPriceModel.TLDTypeId : 0,
                        Status = item.Status
                    };
                    domainApiInfoList.DomainApiResponseInfoList.Add(domainApiInfoModel);
                }
                response.DomainAvailabilityInfoList = null;
            }
            else
            {
                domainApiInfoList.Message = "Bilinmeyen bir hata ile karşılaşıldı!";
            }
            return View(domainApiInfoList);
        }

        [HttpPost]
        public ActionResult AddDomainInfosToCache(DomainApiResponseInfoViewModel model)
        {
            DomainApplicationWrapperViewModelSession.DomainApplicationDetailViewModel
                = new DomainApplicationDetailViewModel
                {
                    DomainName = model.DomainName,
                    Period = 1,
                    DomainOperationTypeId = (int)EnumDomainApplicationType.Ilk_Basvuru,
                    TLDTypeId = model.TldId,
                    DomainStateId = (int)EnumDomainState.Inactive,
                    IpAddress = FindIpAddress(),
                    Price = model.Price.Value,
                    Currency = model.Currency
                };

            var dict = new Dictionary<string, string>
            {
                { "ProductName", model.DomainName },
                { "Price", model.Price.ToString() },
                { "Currency", model.Currency }
            };

            return new JsonResult()
            {
                Data = dict
            };
        }
    }
}