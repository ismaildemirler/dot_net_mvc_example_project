using Domain.Business.Abstract.Parameter;
using Domain.Business.Abstract.Slider;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Parameter;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Areas.Admin.Model.Parameter;
using Domain.Web.Models.Slider;
using Domain.WebHelpers.Infrastructre;
using Domain.WebHelpers.Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        #region Slider Content

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSliderContentPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);

            PagedList<SliderContentComplexType> lst = _sliderService.GetSliderContentPagedList(dtParameterModel, filters);
            return Json(new
            {
                data = lst.Items.Select(s => new SliderContentViewModel()
                {
                    Header = s.Header,
                    Topic = s.Topic,
                    HasPriceSectionDesc = s.HasPriceSection ? "Evet" : "Hayır",
                    StateDesc = (s.StateId == (byte)EnumState.Active ? "Aktif" : "Pasif"),
                    CreationDate = s.CreationDate,
                    Buttons = ApplicationButtons(s),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string ApplicationButtons(SliderContentComplexType entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a class='dropdown-item grid-btn-insert text-primary' data-action='{Url.Action("Update", new { id = entity.SliderContentId })}'> <i class='fa fa-edit'> </i> Güncelle </a>");
            
            if(entity.StateId == (byte)EnumState.Active)
                sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' success-callback='Listele' data-url='{Url.Action("ChangeSliderContentState", new { id = entity.SliderContentId, stateId = (byte)EnumState.InActive })}'> <i class='fa fa-times'> </i> Pasif Yap </a>");
            else if (entity.StateId == (byte)EnumState.InActive)
                sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-success' data-swal-text='Seçili kayıt aktif duruma getirilecek.' success-callback='Listele' data-url='{Url.Action("ChangeSliderContentState", new { id = entity.SliderContentId, stateId = (byte)EnumState.Active })}'> <i class='fa fa-check'> </i> Aktif Yap </a>");

            return sb.ToString();
        }

        [HttpGet]
        public ActionResult Update(Guid? id = null)
        {
            SliderContentViewModel model = new SliderContentViewModel();
            
            if(id != null)
            {
                var response = _sliderService.GetSliderContent(id.Value);

                model.SliderContentId = response.SliderContentId.Encrypt();
                model.CurrencyTypeId = (EnumCurrencyType?)response.CurrencyTypeId;
                model.DetailUrl = response.DetailUrl;
                model.DurationTypeId = (EnumDurationType?)response.DurationTypeId;
                model.HasDetailLink = response.HasDetailLink;
                model.HasPriceSection = response.HasPriceSection;
                model.Header = response.Header;
                model.Price = response.Price;
                model.PriceDescription = response.PriceDescription;
                model.Topic = response.Topic;
                model.SliderImage = response.SliderImage;

                ViewBag.NewRecord = false;
            }
            else
            {
                model.SliderContentId = Guid.Empty.Encrypt();
                ViewBag.NewRecord = true;
            }

            return View(model);
        }   

        [HttpPost]
        public ActionResult Update(SliderContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            var result = Guid.TryParse(model.SliderContentId.Decrypt(), out Guid id);
            if(!result)
            {
                ShowMessage(Messages.KayitBulunamadi, MessageType.warning);
                return View(model);
            }

            byte[] image = null;
            if (model.FileSlider != null && model.FileSlider.ContentLength > 0)
            {
                if (model.FileSlider.ContentType.ToLower() != "image/jpg" &&
                model.FileSlider.ContentType.ToLower() != "image/jpeg" &&
                model.FileSlider.ContentType.ToLower() != "image/pjpeg" &&
                model.FileSlider.ContentType.ToLower() != "image/gif" &&
                model.FileSlider.ContentType.ToLower() != "image/x-png" &&
                model.FileSlider.ContentType.ToLower() != "image/png")
                {
                    ShowMessage(Messages.GecersizResimUzantisi, MessageType.warning);
                    return View(model);
                }

                Stream fs = model.FileSlider.InputStream;
                BinaryReader br = new BinaryReader(fs);
                image = br.ReadBytes((Int32)fs.Length);
            }

            id = _sliderService.SaveSliderContent(new SliderContentComplexType()
            {
                SliderContentId = id,
                CurrencyTypeId = (byte?)model.CurrencyTypeId,
                DetailUrl = model.DetailUrl,
                DurationTypeId = (byte?)model.DurationTypeId,
                HasDetailLink = model.HasDetailLink,
                HasPriceSection = model.HasPriceSection,
                Header = model.Header,
                Price = model.Price,
                PriceDescription = model.PriceDescription,
                Topic = model.Topic,
                SliderImage = image,
                ImageDeleted = model.ImageDeleted
            });

            ShowMessage(Messages.KayitBasarili);
            return RedirectToAction("Update", new { id = id });
        }

        [HttpPost]
        public ActionResult ChangeSliderContentState(Guid id, byte stateId)
        {
            _sliderService.ChangeSliderContentState(id, stateId);
            CacheManager.GetAllSliderContent = _sliderService.GetActiveSliderContents();
            return ShowMessageJSON(Messages.SilmeBasarili, AjaxResultState.Success);
        }


        #endregion

        #region Slider Content Detail

        [HttpPost]
        public ActionResult GetSliderContentDetailList(string sliderContentId, DtParameterModel dtParameterModel)
        {
            var detailList = _sliderService.GetSliderContentDetails(Guid.Parse(sliderContentId.Decrypt()));
            return Json(new
            {
                data = detailList.Select(s => new SliderContentDetailViewModel()
                {
                    ContentInfo = s.ContentInfo,
                    RankNumber = s.RankNumber,
                    Buttons = DetailButtons(s),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = detailList.Count,
                recordsFiltered = detailList.Count
            });
        }

        [NonAction]
        private string DetailButtons(SliderContentDetail entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a class='dropdown-item update text-primary' success-callback='GetDetailList' data-url='{Url.Action("_UpdateSliderContentDetail", new { id = entity.SliderContentDetailId })}'> <i class='fa fa-edit'> </i> Güncelle </a>");
            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' success-callback='GetDetailList' data-url='{Url.Action("DeleteSliderContentDetail", new { id = entity.SliderContentDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }

        [HttpGet]
        public ActionResult _UpdateSliderContentDetail(string sliderContentId = "", Guid? id = null)
        {
            SliderContentDetailViewModel model = new SliderContentDetailViewModel();

            if (id.HasValue)
            {
                var response = _sliderService.GetSliderContentDetail(id.Value);
                model = new SliderContentDetailViewModel
                {
                    SliderContentId = response.SliderContentId.Encrypt(),
                    SliderContentDetailId = response.SliderContentDetailId,
                    ContentInfo = response.ContentInfo,
                    RankNumber = response.RankNumber
                };
            }
            else
            {
                if (string.IsNullOrEmpty(sliderContentId))
                    throw new BusinessException(Messages.GecersizUrl);

                model.SliderContentDetailId = Guid.Empty;
                model.SliderContentId = sliderContentId;
            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _UpdateSliderContentDetail(SliderContentDetailViewModel model)
        {
            _sliderService.SaveSliderContentDetail(new SliderContentDetailComplexType { 
                SliderContentId = Guid.Parse(model.SliderContentId.Decrypt()),
                SliderContentDetailId = model.SliderContentDetailId,
                ContentInfo = model.ContentInfo,
                RankNumber = model.RankNumber
            });
            return ShowMessageJSON(Messages.KayitBasarili, AjaxResultState.Success);
        }

        [HttpPost]
        public ActionResult DeleteSliderContentDetail(Guid id)
        {
            _sliderService.DeleteSliderContentDetail(id);
            return ShowMessageJSON(Messages.SilmeBasarili, AjaxResultState.Success);
        }

        #endregion
    }
}