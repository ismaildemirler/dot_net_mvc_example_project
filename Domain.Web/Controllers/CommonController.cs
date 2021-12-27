using Domain.Business.Abstract.Attachment;
using Domain.Business.Abstract.Blog;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Attachment;
using Domain.Entity.Enum;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Web.Models.Attachment;
using Domain.WebHelpers.Infrastructre;
using Domain.WebHelpers.Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Domain.WebHelpers.Infrastructre.Enums;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class CommonController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IAttachmentService _attachmentService;

        public CommonController(IBlogService blogService,
            IAttachmentService attachmentService)
        {
            _blogService = blogService;
            _attachmentService = attachmentService;
        }

        public ActionResult _Blogs(int pageIndex = 1, int? categoryId = null)
        {
            PagedList<BlogContentComplexType> model = _blogService.GetBlogPagedList(pageIndex - 1, categoryId);
            return PartialView(model);
        }

        #region Attachments
        public ActionResult Attachments(AttachmentViewModel model)
        {
            return PartialView("_Attachment", model);
        }

        public ActionResult AttachmentList(AttachmentViewModel model)
        {
            var attachments = _attachmentService.GetAttachmentsWithoutData(new AttachmentComplexType
            {
                AttachmentId = model.AttachmentId,
                RelatedEntityId = model.RelatedEntityId,
                RelatedEntity = model.RelatedEntity,
                FormType = EnumFormType.Nothing
            });

            var forms = _attachmentService.GetForms(new AttachmentComplexType
            {
                RelatedEntity = model.RelatedEntity
            });

            return PartialView("_AttachmentList", new AttachmentListViewModel
            {
                Items = attachments.Select(s => new AttachmentItemsViewModel
                {
                    AttachmentFileName = s.AttachmentFileName,
                    AttachmentId = s.AttachmentId,
                    AttachmentTypeName = s.AttachmentTypeName,
                    FormId = s.FormId
                }).ToList(),
                Forms = forms.Select(s => new Form
                {
                    FormId = s.FormId,
                    FormName = s.FormName
                }).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttachmentUpload(HttpPostedFileBase uploadedFile,
            AttachmentViewModel formAttachment, int formId)
        {
            if (uploadedFile == null)
            {
                return ShowMessageJSON("Dosya yüklemelisiniz!", AjaxResultState.Fail);
            }

            ActionResult result = null;
            List<EnumFileExtensionTypes> permittedExtensions = null;
            var attachmentTypeId = formAttachment.RelatedEntity;
            switch (attachmentTypeId)
            {
                case EnumRelatedEntity.PatentApplicationDetail:
                    permittedExtensions = new List<EnumFileExtensionTypes>
                    {
                        EnumFileExtensionTypes.pdf
                    };
                    break;
                case EnumRelatedEntity.IndustrialDesignApplicationDetail:
                case EnumRelatedEntity.IndustrialDesignApplicationPhotographs:
                    permittedExtensions = new List<EnumFileExtensionTypes>
                    {
                        EnumFileExtensionTypes.png,
                        EnumFileExtensionTypes.jpeg,
                        EnumFileExtensionTypes.jpg
                    };
                    break;
            }

            result = ValidateFormFileUpload(uploadedFile.ContentType.ToLower(),
                uploadedFile.ContentLength, uploadedFile.FileName, 26214400, permittedExtensions);

            if (result != null)
                return result;

            var attachment = new Attachment
            {
                AttachmentData = new BinaryReader(uploadedFile.InputStream).ReadBytes(uploadedFile.ContentLength),
                AttachmentFileName = uploadedFile.FileName,
                AttachmentTypeName = uploadedFile.ContentType,
                RelatedEntity = (int)formAttachment.RelatedEntity,
                RelatedEntityId = formAttachment.RelatedEntityId,
                FormId = formId,
                AttachmentId = Guid.NewGuid()
            };

            var response = _attachmentService.SaveAttachment(new RequestAttachment
            {
                Attachment = attachment
            })?.Attachment;

            AttachmentSession = response;

            return Json(new { id = AttachmentSession.AttachmentId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AttachmentDownload(Guid attachmentId)
        {
            var attachment = _attachmentService.GetAttachments(new AttachmentComplexType
            {
                AttachmentId = attachmentId
            })?.FirstOrDefault();
            return File(attachment.AttachmentData, "application/octet-stream", attachment.AttachmentFileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttachmentDelete(Guid attachmentId)
        {
            _attachmentService.DeleteAttachment(
                new RequestAttachment
                {
                    Attachment = new Attachment
                    {
                        AttachmentId = attachmentId
                    }
                });
            return Json(true);
        }

        [HttpPost]
        public ActionResult MultipleImageUpload(EnumRelatedEntity relatedEntity,
            Guid relatedEntityId, int formId)
        {
            bool isSavedSuccessfully = false;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    ActionResult result = null;
                    List<EnumFileExtensionTypes> permittedExtensions = null;
                    var attachmentTypeId = relatedEntity;
                    switch (attachmentTypeId)
                    {
                        case EnumRelatedEntity.PatentApplicationDetail:
                            permittedExtensions = new List<EnumFileExtensionTypes>()
                            {
                                EnumFileExtensionTypes.pdf
                            };
                            break;
                        case EnumRelatedEntity.IndustrialDesignApplicationDetail:
                        case EnumRelatedEntity.IndustrialDesignApplicationPhotographs:
                            permittedExtensions = new List<EnumFileExtensionTypes>
                            {
                                EnumFileExtensionTypes.png,
                                EnumFileExtensionTypes.jpeg,
                                EnumFileExtensionTypes.jpg
                            };
                            break;
                    }

                    HttpPostedFileBase file = Request.Files[fileName];

                    result = ValidateFormFileUpload(file.ContentType.ToLower(),
                        file.ContentLength, file.FileName, 26214400, permittedExtensions);

                    if (result != null)
                        return result;

                    var attachment = new Attachment
                    {
                        AttachmentData = new BinaryReader(file.InputStream).ReadBytes(file.ContentLength),
                        AttachmentFileName = file.FileName,
                        AttachmentTypeName = file.ContentType,
                        RelatedEntity = (int)relatedEntity,
                        RelatedEntityId = relatedEntityId,
                        FormId = formId,
                        AttachmentId = Guid.NewGuid()
                    };

                    var response = _attachmentService.SaveAttachment(new RequestAttachment
                    {
                        Attachment = attachment
                    })?.Attachment;

                    isSavedSuccessfully = true;
                }
                return Json(new { isSavedSuccessfully, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetMultipleUploadedImage(MultipleAttachmentViewModel model)
        {
            try
            {
                var forms = _attachmentService.GetForms(new AttachmentComplexType
                {
                    RelatedEntity = model.RelatedEntity
                });

                var attachments = _attachmentService.GetAttachments(
                    new AttachmentComplexType
                    {
                        RelatedEntityId = model.RelatedEntityId,
                        FormType = model.FormType
                    });

                return PartialView("_MultipleAttachmentList", new MultipleAttachmentListViewModel
                {
                    Items = attachments.Select(s => new MultipleAttachmentItemsViewModel
                    {
                        AttachmentFileName = s.AttachmentFileName,
                        AttachmentId = s.AttachmentId,
                        AttachmentTypeName = s.AttachmentTypeName,
                        AttachmentData = s.AttachmentData,
                        FormId = s.FormId
                    }).ToList(),
                    Forms = forms.Select(s => new Form
                    {
                        FormId = s.FormId,
                        FormName = s.FormName
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region City/Town
        public ActionResult GetTownByCityId(int cityId)
        {
            var towns = CacheManager.GetAllTowns.Where(f => f.CityId == cityId).ToList();
            return Json(new { success = true, towns = towns }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private ActionResult ShowMessageJSON(string v, object fail)
        {
            throw new NotImplementedException();
        }
    }
}