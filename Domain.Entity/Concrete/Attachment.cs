using Domain.Infrastructure.Entities;
using System;

namespace Domain.Entity.Concrete
{
    public class Attachment : IEntity
    {
        public Guid AttachmentId { get; set; }
        public int RelatedEntity { get; set; }
        public Guid RelatedEntityId { get; set; }
        public byte[] AttachmentData { get; set; }
        public string AttachmentTypeName { get; set; }
        public string AttachmentFileName { get; set; }
        public int FormId { get; set; }
    }
}
