using Domain.Infrastructure.CrossCuttingConcerns.Session;
using Domain.Web.Areas.User.Models.Brand;
using Domain.Web.Areas.User.Models.Patent;
using Domain.WebHelpers.Infrastructre;
using System.Web.Mvc;

namespace Domain.Web.Infrastructure
{
    public class DomainBaseController : BaseController
    {
        public BrandApplicationViewModel BrandApplicationViewModelSession
        {
            get
            {
                if (SessionManager.Get("BrandApplicationViewModelSession") == null)
                    SessionManager.Set("BrandApplicationViewModelSession", new BrandApplicationViewModel());
                return (BrandApplicationViewModel)SessionManager.Get("BrandApplicationViewModelSession");

            }
            set => SessionManager.Set("BrandApplicationViewModelSession", value);
        }

        public PatentApplicationViewModel PatentApplicationViewModelSession
        {
            get
            {
                if (SessionManager.Get("PatentApplicationViewModelSession") == null)
                    SessionManager.Set("PatentApplicationViewModelSession", new PatentApplicationViewModel());
                return (PatentApplicationViewModel)SessionManager.Get("PatentApplicationViewModelSession");

            }
            set => SessionManager.Set("PatentApplicationViewModelSession", value);
        }
    }
}