using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Slider
{
    public interface ISliderService
    {
        List<SliderContent> GetActiveSliderContents();

        List<SliderContentDetail> GetActiveSliderContentDetails();

        PagedList<SliderContentComplexType> GetSliderContentPagedList(DtParameterModel dtParameterModel, List<SearchFilter> filters);

        SliderContentComplexType GetSliderContent(Guid sliderContentId);

        Guid SaveSliderContent(SliderContentComplexType sliderContent);

        SliderContentDetailComplexType GetSliderContentDetail(Guid sliderContentDetailId);

        void SaveSliderContentDetail(SliderContentDetailComplexType sliderContentDetail);

        List<SliderContentDetail> GetSliderContentDetails(Guid sliderContentId);

        void ChangeSliderContentState(Guid sliderContentId, byte stateId);

        void DeleteSliderContentDetail(Guid sliderContentDetailId);
    }
}
