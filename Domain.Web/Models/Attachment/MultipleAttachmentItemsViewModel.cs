using System;

namespace Domain.Web.Models.Attachment
{
    public class MultipleAttachmentItemsViewModel
    {
        public Guid AttachmentId { get; set; }
        public int RelatedEntity { get; set; }
        public int RelatedEntityId { get; set; }
        public byte[] AttachmentData { get; set; }
        public string AttachmentTypeName { get; set; }
        public string AttachmentFileName { get; set; }
        public int FormId { get; set; }
    }
}