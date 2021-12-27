using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Web.Models.Attachment
{
    public class AttachmentListViewModel
    {
        public List<AttachmentItemsViewModel> Items { get; set; }
        public List<Form> Forms { get; set; }
    }
}