
using System.Collections.Generic;

namespace Domain.Web.Models.Domain
{
    public class DomainApiResponseInfoListViewModel
    {
        public DomainApiResponseInfoListViewModel()
        {
            DomainApiResponseInfoList = new List<DomainApiResponseInfoViewModel>();
        }
        public List<DomainApiResponseInfoViewModel> DomainApiResponseInfoList { get; set; }
        public string Message { get; set; }
    }
}