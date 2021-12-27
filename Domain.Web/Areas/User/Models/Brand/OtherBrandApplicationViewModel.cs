using Domain.Entity.Concrete;
using Domain.Entity.ViewModels.UserBrandApplication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.User.Models.Brand
{
    public class OtherBrandApplicationViewModel
    {
        public OtherBrandApplicationViewModel()
        {
            BrandClassesArray = "";
            Contacts = new List<ContactOtherViewModel>();
            CustomerApplication = new CustomerApplication();
            BrandApplicationDetail = new BrandApplicationDetailOtherViewModel();
            Contact = new ContactOtherViewModel();
            Beneficiary = new BeneficiaryOtherViewModel();
        }

        [Required]
        [Display(Name = "Marka Sınıfları *")]
        public string BrandClassesArray { get; set; }
        public ContactOtherViewModel Contact { get; set; }
        public List<ContactOtherViewModel> Contacts { get; set; }
        public BeneficiaryOtherViewModel Beneficiary { get; set; }
        public CustomerApplication CustomerApplication { get; set; }
        public BrandApplicationDetailOtherViewModel BrandApplicationDetail { get; set; }
    }
}