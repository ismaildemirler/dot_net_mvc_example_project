using Domain.Entity.Concrete;
using Domain.Entity.ViewModels.UserIndustrialDesignApplication;

namespace Domain.Web.Areas.User.Models.IndustrialDesign
{
    public class IndustrialDesignApplicationViewModel
    {
        public IndustrialDesignApplicationViewModel()
        {
            CustomerApplication = new CustomerApplication();
            IndustrialDesignApplicationDetail = new IndustrialDesignApplicationDetailViewModel();
        }

        public CustomerApplication CustomerApplication { get; set; }
        public IndustrialDesignApplicationDetailViewModel IndustrialDesignApplicationDetail { get; set; }
    }
}