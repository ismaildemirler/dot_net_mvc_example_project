using Domain.Entity.Concrete;
using Domain.Entity.ViewModels.UserBrandApplication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.User.Models.Brand
{
    public class BrandApplicationViewModel
    {
        public BrandApplicationViewModel()
        {
            BrandClassesArray = "";
            Contact = new ContactViewModel();
            Beneficiary = new BeneficiaryViewModel();
            CustomerApplication = new CustomerApplication();
            BrandApplicationDetail = new BrandApplicationDetailViewModel();
            Contacts = new List<ContactViewModel>();
        }

        [Required(ErrorMessage = "Marka Sınıfları zorunlu alandır")]
        [Display(Name = "Marka Sınıfları *")]
        public string BrandClassesArray { get; set; }
        public ContactViewModel Contact { get; set; }
        public List<ContactViewModel> Contacts { get; set; }
        public BeneficiaryViewModel Beneficiary { get; set; }
        public CustomerApplication CustomerApplication { get; set; }
        public BrandApplicationDetailViewModel BrandApplicationDetail { get; set; }
    }
}