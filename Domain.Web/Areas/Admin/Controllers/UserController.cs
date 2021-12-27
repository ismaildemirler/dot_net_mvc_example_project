using Domain.Business.Abstract.Common;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Areas.Admin.Model.User;
using Domain.WebHelpers.Infrastructre;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly ISystemUserService _systemUserService;

        public UserController(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUserPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);

            var pagedList = _systemUserService.GetUserPagedList(dtParameterModel, filters);
            var data = pagedList.Items.Select(s => new SystemUserViewModel()
            {
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNumber = s.PhoneNumber,
                State = s.State,
                SystemUserId = s.SystemUserId.Encrypt(),
                UserType = s.UserType,
                CreationDate = s.CreationDate,
                LastLoginDate = s.LastLoginDate,
                Buttons = UserRowButton(s)
            });
            
            return Json(new
            {
                data = data,
                draw = dtParameterModel.Draw,
                recordsTotal = pagedList.TotalCount,
                recordsFiltered = pagedList.TotalCount,
            });
        }

        private string UserRowButton(SystemUserComplexType response)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append($" <a class='dropdown-item grid-btn-delete text-danger' success-callback='UserList' data-url='{Url.Action("ChangeUserState", new { id = response.SystemUserId.Encrypt() })}'> <i class='fa fa-times'> </i> Aktif Yap </a>");
            //sb.Append($" <a class='dropdown-item grid-btn-delete text-danger' success-callback='UserList' data-url='{Url.Action("ChangeUserState", new { id = response.SystemUserId.Encrypt() })}'> <i class='fa fa-times'> </i> Pasif Yap </a>");
            return sb.ToString();
        }
    }
}