
using System;

namespace Domain.Entity.Container.Response.Attachment
{
    public class ResponseAttachment
    {
        public Concrete.Attachment Attachment { get; set; }
    }

    public class ResponseAttachmentWithoutData
    {
        public Guid AttachmentId { get; set; }
        public int RelatedEntity { get; set; }
        public Guid RelatedEntityId { get; set; }
        public string AttachmentTypeName { get; set; }
        public string AttachmentFileName { get; set; }
        public int FormId { get; set; }
    }
}
