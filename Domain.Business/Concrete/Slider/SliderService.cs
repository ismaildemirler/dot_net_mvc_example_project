using Domain.Business.Abstract.Patent;
using Domain.Business.Abstract.Slider;
using Domain.DataAccess.Abstract.Patent;
using Domain.DataAccess.Abstract.Slider;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Patent;
using Domain.Entity.Container.Response.Patent;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Slider
{
    public class SliderService : ISliderService
    {
        private readonly ISliderDAL _sliderDAL;

        public SliderService(ISliderDAL sliderDAL)
        {
            _sliderDAL = sliderDAL;
        }

        public List<SliderContent> GetActiveSliderContents()
        {
            return _sliderDAL.GetRepository<SliderContent>().Queryable().Where(w => w.StateId == (byte)EnumState.Active).ToList();
        }

        public List<SliderContentDetail> GetActiveSliderContentDetails()
        {
            var sliderContentIds = _sliderDAL.GetRepository<SliderContent>().Queryable().Where(w => w.StateId == (byte)EnumState.Active).Select(s => s.SliderContentId).ToList();
            return _sliderDAL.GetRepository<SliderContentDetail>().Queryable().Where(w => sliderContentIds.Contains(w.SliderContentId)).ToList();
        }

        public PagedList<SliderContentComplexType> GetSliderContentPagedList(DtParameterModel dtParameterModel, List<SearchFilter> filters)
        {
            var rep = _sliderDAL.GetRepository<SliderContent>();
            return rep.GetPagedList<SliderContentComplexType>(rep.Queryable().Select(s=> new SliderContentComplexType { 
                CreationDate = s.CreationDate,
                CurrencyTypeId = s.CurrencyTypeId,
                DetailUrl = s.DetailUrl,
                DurationTypeId = s.DurationTypeId,
                HasDetailLink = s.HasDetailLink,
                HasPriceSection = s.HasPriceSection,
                Header = s.Header,
                Price = s.Price,
                PriceDescription = s.PriceDescription,
                SliderContentId = s.SliderContentId,
                StateId = s.StateId,
                Topic = s.Topic
            }).OrderByDescending(o=>o.CreationDate), dtParameterModel.AramaKriteri.Baslangic.Value, dtParameterModel.AramaKriteri.Uzunluk.Value, true);
        }

        public SliderContentComplexType GetSliderContent(Guid sliderContentId)
        {
            var sliderContent = _sliderDAL.GetRepository<SliderContent>().Queryable().Where(w => w.SliderContentId == sliderContentId).FirstOrDefault();
            if (sliderContent == null)
                throw new BusinessException(Messages.KayitBulunamadi);

            return new SliderContentComplexType()
            {
                CurrencyTypeId = sliderContent.CurrencyTypeId,
                DetailUrl = sliderContent.DetailUrl,
                DurationTypeId = sliderContent.DurationTypeId,
                HasDetailLink = sliderContent.HasDetailLink,
                HasPriceSection = sliderContent.HasPriceSection,
                Header = sliderContent.Header,
                Price = sliderContent.Price,
                PriceDescription = sliderContent.PriceDescription,
                SliderContentId = sliderContent.SliderContentId,
                StateId = sliderContent.StateId,
                SliderImage = sliderContent.SliderImage,
                Topic = sliderContent.Topic
            };
        }

        public Guid SaveSliderContent(SliderContentComplexType sliderContent)
        {
            var rep = _sliderDAL.GetRepository<SliderContent>();

            if (sliderContent.SliderContentId != Guid.Empty)
            {
                var sliderContentEntity = rep.Find(sliderContent.SliderContentId);
                if (sliderContentEntity == null)
                    throw new BusinessException(Messages.KayitBulunamadi);
                else
                {
                    sliderContentEntity.CurrencyTypeId = sliderContent.CurrencyTypeId;
                    sliderContentEntity.DetailUrl = sliderContent.DetailUrl;
                    sliderContentEntity.DurationTypeId = sliderContent.DurationTypeId;
                    sliderContentEntity.HasDetailLink = sliderContent.HasDetailLink;
                    sliderContentEntity.HasPriceSection = sliderContent.HasPriceSection;
                    sliderContentEntity.Header = sliderContent.Header;
                    sliderContentEntity.Price = sliderContent.Price;
                    sliderContentEntity.PriceDescription = sliderContent.PriceDescription;
                    sliderContentEntity.Topic = sliderContent.Topic;
                    
                    if(sliderContent.SliderImage != null || (sliderContent.SliderImage == null && sliderContent.ImageDeleted))
                    {
                        sliderContentEntity.SliderImage = sliderContent.SliderImage;
                    }

                    rep.Update(sliderContentEntity);
                    return sliderContentEntity.SliderContentId;
                }
            }
            else
            {
                var sliderContentEntity = new SliderContent()
                {
                    SliderContentId = Guid.NewGuid(),
                    CurrencyTypeId = sliderContent.CurrencyTypeId,
                    DetailUrl = sliderContent.DetailUrl,
                    DurationTypeId = sliderContent.DurationTypeId,
                    HasDetailLink = sliderContent.HasDetailLink,
                    HasPriceSection = sliderContent.HasPriceSection,
                    Header = sliderContent.Header,
                    Price = sliderContent.Price,
                    PriceDescription = sliderContent.PriceDescription,
                    Topic = sliderContent.Topic,
                    SliderImage = sliderContent.SliderImage,
                    StateId = (byte)EnumState.Active,
                    CreationDate = DateTime.Now
                };

                rep.Insert(sliderContentEntity);
                return sliderContentEntity.SliderContentId;
            }
        }

        public SliderContentDetailComplexType GetSliderContentDetail(Guid sliderContentDetailId)
        {
            var sliderContentDetail = _sliderDAL.GetRepository<SliderContentDetail>().Queryable().Where(w => w.SliderContentDetailId == sliderContentDetailId).FirstOrDefault();
            if (sliderContentDetail == null)
                throw new BusinessException(Messages.KayitBulunamadi);

            return new SliderContentDetailComplexType()
            {
                SliderContentDetailId = sliderContentDetail.SliderContentDetailId,
                SliderContentId = sliderContentDetail.SliderContentId,
                ContentInfo = sliderContentDetail.ContentInfo,
                RankNumber = sliderContentDetail.RankNumber,
            };
        }

        public void SaveSliderContentDetail(SliderContentDetailComplexType sliderContentDetail)
        {
            var rep = _sliderDAL.GetRepository<SliderContentDetail>();

            if (sliderContentDetail.SliderContentDetailId != Guid.Empty)
            {
                var sliderContentDetailEntity = rep.Find(sliderContentDetail.SliderContentDetailId);
                if (sliderContentDetailEntity == null)
                    throw new BusinessException(Messages.KayitBulunamadi);
                else
                {
                    sliderContentDetailEntity.ContentInfo = sliderContentDetail.ContentInfo;
                    sliderContentDetailEntity.RankNumber = sliderContentDetail.RankNumber;

                    rep.Update(sliderContentDetailEntity);
                }
            }
            else
            {
                var sliderContentDetailEntity = new SliderContentDetail()
                {
                    SliderContentDetailId = Guid.NewGuid(),
                    SliderContentId = sliderContentDetail.SliderContentId,
                    ContentInfo = sliderContentDetail.ContentInfo,
                    RankNumber = sliderContentDetail.RankNumber
                };

                rep.Insert(sliderContentDetailEntity);
            }
        }

        public List<SliderContentDetail> GetSliderContentDetails(Guid sliderContentId)
        {
            return _sliderDAL.GetRepository<SliderContentDetail>().Queryable().Where(w => w.SliderContentId == sliderContentId).OrderBy(o=>o.RankNumber).ToList();
        }

        public void ChangeSliderContentState(Guid sliderContentId, byte stateId)
        {
            var rep = _sliderDAL.GetRepository<SliderContent>();

            var sliderContent = rep.Find(sliderContentId);
            if (sliderContent == null)
                throw new BusinessException(Messages.KayitBulunamadi);
            else
            {
                sliderContent.StateId = stateId;
                rep.Update(sliderContent);
            }
        }

        public void DeleteSliderContentDetail(Guid sliderContentDetailId)
        {
            var rep = _sliderDAL.GetRepository<SliderContentDetail>();

            var sliderContentDetail = rep.Find(sliderContentDetailId);
            if (sliderContentDetail == null)
                throw new BusinessException(Messages.KayitBulunamadi);
            else
            {
                rep.Delete(sliderContentDetail);
            }
        }
    }
}
