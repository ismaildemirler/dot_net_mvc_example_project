using Domain.Business.Abstract.ReferenceFirm;
using Domain.Entity.Concrete;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class ReferenceFirmController : Controller
    {
        private readonly IReferenceFirmService _referenceFirmService;
        public ReferenceFirmController(IReferenceFirmService referenceFirmService)
        {
            _referenceFirmService = referenceFirmService;
        }

        public ActionResult Index(int pageIndex = 1, int? categoryId = null)
        {
            PagedList<ReferenceFirm> firms = _referenceFirmService.GetActiveReferenceFirmPagedList(pageIndex - 1);
            return View(firms);
        }
    }
}