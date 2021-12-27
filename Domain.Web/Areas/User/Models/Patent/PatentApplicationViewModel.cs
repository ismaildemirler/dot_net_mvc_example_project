using Domain.Entity.Concrete;
using Domain.Entity.ViewModels.UserPatentApplication;

namespace Domain.Web.Areas.User.Models.Patent
{
    public class PatentApplicationViewModel
    {
        public PatentApplicationViewModel()
        {
            Beneficiary = new BeneficiaryPatentViewModel();
            CustomerApplication = new CustomerApplication();
            PatentApplicationDetail = new PatentApplicationDetailViewModel();
        }

        public BeneficiaryPatentViewModel Beneficiary { get; set; }
        public CustomerApplication CustomerApplication { get; set; }
        public PatentApplicationDetailViewModel PatentApplicationDetail { get; set; }
    }
}