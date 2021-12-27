using Domain.Business.Abstract.Brand;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetBrandApplicationPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);

            var lst = _brandService.GetPagedListSearchedBrandApplicationCT(dtParameterModel, filters);
            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.BrandApplicationDetailId,
                    p.BrandName,
                    p.ApplicationTypeDescription,
                    p.FirmName,
                    Buttons = ApplicationButtons(p),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string ApplicationButtons(BrandApplicationComplexType entity)
        {
            var sb = new StringBuilder();
            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("ApproveBrandApplication", "Brand", new { area = "Admin", brandApplicationDetailId = entity.BrandApplicationDetailId })}'><i class=\"fa fa-edit\"></i>&nbsp;Onayla</a>");
            return sb.ToString();
        }
    }
}