using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Attachment;
using Domain.Entity.Container.Response.Attachment;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Attachment
{
    public interface IAttachmentService
    {
        ResponseAttachment SaveAttachment(RequestAttachment request);
        List<Form> GetForms(AttachmentComplexType request);
        void DeleteAttachment(RequestAttachment request);
        List<Entity.Concrete.Attachment> GetAttachments(AttachmentComplexType request);
        List<ResponseAttachmentWithoutData> GetAttachmentsWithoutData(AttachmentComplexType request);
    }
}
