using Domain.Business.Abstract.Beneficiary;
using System.Web.Mvc;
using Domain.Business.Abstract.Contact;
using Domain.Business.Abstract.Customer;
using Domain.Business.Abstract.Patent;
using Domain.Business.Abstract.Attachment;
using Domain.Web.Infrastructure;

namespace Domain.Web.Areas.User.Controllers
{
    public class DomainController : DomainBaseController
    {
        private readonly IPatentService _patentService;
        private readonly IContactService _contactService;
        private readonly ICustomerService _customerService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAttachmentService _attachmentService;

        public DomainController(IPatentService patentService,
            IBeneficiaryService beneficiaryService,
            IContactService contactService,
            ICustomerService customerService,
            IAttachmentService attachmentService)
        {
            _contactService = contactService;
            _patentService = patentService;
            _beneficiaryService = beneficiaryService;
            _customerService = customerService;
            _attachmentService = attachmentService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}