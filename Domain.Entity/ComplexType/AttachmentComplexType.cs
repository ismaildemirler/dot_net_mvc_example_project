using Domain.Entity.Enum;
using System;

namespace Domain.Entity.ComplexType
{
    public class AttachmentComplexType
    {
        public Guid AttachmentId { get; set; }
        public Guid RelatedEntityId { get; set; }
        public EnumRelatedEntity RelatedEntity { get; set; }
        public byte[] AttachmentData { get; set; }
        public string AttachmentTypeName { get; set; }
        public string AttachmentFileName { get; set; }
        public EnumFormType FormType { get; set; }
    }
}
