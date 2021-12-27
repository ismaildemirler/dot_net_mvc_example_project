using Domain.Entity.Concrete;
using Domain.Entity.ViewModels.UserBrandForWatching;
using System.Collections.Generic;

namespace Domain.Web.Areas.User.Models.Brand
{
    public class BrandWatchingApplicationViewModel
    {
        public BrandWatchingApplicationViewModel()
        {
            BrandForWatching = new BrandForWatchingViewModel();
            CustomerApplication = new CustomerApplication();
            BrandForWatchings = new List<BrandForWatchingViewModel>();
            BrandWatchingApplicationDetail = new BrandWatchingApplicationDetailViewModel();
        }

        public BrandForWatchingViewModel BrandForWatching { get; set; }
        public CustomerApplication CustomerApplication { get; set; }
        public List<BrandForWatchingViewModel> BrandForWatchings { get; set; }
        public BrandWatchingApplicationDetailViewModel BrandWatchingApplicationDetail { get; set; }
    }
}