using Domain.Business.Abstract.Attachment;
using Domain.DataAccess.Abstract.Attachment;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Attachment;
using Domain.Entity.Container.Response.Attachment;
using Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentDAL _attachmentDAL;

        public AttachmentService(IAttachmentDAL attachmentDAL)
        {
            _attachmentDAL = attachmentDAL;
        }

        public List<Entity.Concrete.Attachment> GetAttachments(AttachmentComplexType request)
        {
            var query = _attachmentDAL.GetRepository<Entity.Concrete.Attachment>().Queryable();
            List<Entity.Concrete.Attachment> result = new List<Entity.Concrete.Attachment>();

            if ((request.RelatedEntityId == Guid.Empty
                || request.RelatedEntityId == null)
                && request.AttachmentId != Guid.Empty
                && request.AttachmentId != null)
            {
                query = query.Where(w => w.AttachmentId == request.AttachmentId);
                result = query.ToList();
            }
            else if (request.RelatedEntityId != Guid.Empty
                && request.RelatedEntityId != null)
            {
                query = query.Where(w => w.RelatedEntityId == request.RelatedEntityId);

                if (request.RelatedEntity != EnumRelatedEntity.Nothing)
                    query = query.Where(w => w.RelatedEntity == (int)request.RelatedEntity);

                if (request.FormType != EnumFormType.Nothing)
                    query = query.Where(w => w.FormId == (int)request.FormType);

                result = query.ToList();
            }

            return result;
        }

        public List<ResponseAttachmentWithoutData> GetAttachmentsWithoutData(AttachmentComplexType request)
        {
            var query = _attachmentDAL.GetRepository<Entity.Concrete.Attachment>().Queryable();
            List<ResponseAttachmentWithoutData> result = new List<ResponseAttachmentWithoutData>();

            if ((request.RelatedEntityId == Guid.Empty
                || request.RelatedEntityId == null)
                && request.AttachmentId != Guid.Empty
                && request.AttachmentId != null)
            {
                query = query.Where(w => w.AttachmentId == request.AttachmentId);
                result = query.Select(s=> new ResponseAttachmentWithoutData
                { 
                   AttachmentFileName = s.AttachmentFileName,
                   AttachmentId = s.AttachmentId,
                   AttachmentTypeName = s.AttachmentTypeName,
                   FormId = s.FormId,
                   RelatedEntity = s.RelatedEntity,
                   RelatedEntityId = s.RelatedEntityId
                }).ToList();
            }
            else if (request.RelatedEntityId != Guid.Empty
                && request.RelatedEntityId != null)
            {
                query = query.Where(w => w.RelatedEntityId == request.RelatedEntityId);

                if (request.RelatedEntity != EnumRelatedEntity.Nothing)
                    query = query.Where(w => w.RelatedEntity == (int)request.RelatedEntity);

                if (request.FormType != EnumFormType.Nothing)
                    query = query.Where(w => w.FormId == (int)request.FormType);

                result = query.Select(s => new ResponseAttachmentWithoutData
                {
                    AttachmentFileName = s.AttachmentFileName,
                    AttachmentId = s.AttachmentId,
                    AttachmentTypeName = s.AttachmentTypeName,
                    FormId = s.FormId,
                    RelatedEntity = s.RelatedEntity,
                    RelatedEntityId = s.RelatedEntityId
                }).ToList();
            }

            return result;
        }

        public List<Form> GetForms(AttachmentComplexType request)
        {
            var query = _attachmentDAL.GetRepository<Form>().Queryable();

            if (request.RelatedEntity != EnumRelatedEntity.Nothing)
                query = query.Where(w => w.RelatedEntityId == (int)request.RelatedEntity);

            List<Form> result = query.ToList();
            return result;
        }

        public ResponseAttachment SaveAttachment(RequestAttachment request)
        {
            var attachment = new Entity.Concrete.Attachment
            {
                AttachmentData = request.Attachment.AttachmentData,
                AttachmentFileName = request.Attachment.AttachmentFileName,
                AttachmentTypeName = request.Attachment.AttachmentTypeName,
                RelatedEntity = request.Attachment.RelatedEntity,
                RelatedEntityId = request.Attachment.RelatedEntityId,
                FormId = request.Attachment.FormId,
                AttachmentId = Guid.NewGuid()
            };

            _attachmentDAL.GetRepository<Entity.Concrete.Attachment>().Insert(attachment);
            return new ResponseAttachment
            {
                Attachment = attachment
            };
        }
        public void DeleteAttachment(RequestAttachment request)
        {
            var rep = _attachmentDAL.GetRepository<Entity.Concrete.Attachment>();
            var entity = rep.Queryable().FirstOrDefault(w => w.AttachmentId == request.Attachment.AttachmentId);
            if (entity != null)
                rep.Delete(entity);
        }
    }
}
