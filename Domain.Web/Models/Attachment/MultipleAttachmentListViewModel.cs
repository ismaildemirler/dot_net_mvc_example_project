using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Web.Models.Attachment
{
    public class MultipleAttachmentListViewModel
    {
        public List<MultipleAttachmentItemsViewModel> Items { get; set; }
        public List<Form> Forms { get; set; }
    }
}