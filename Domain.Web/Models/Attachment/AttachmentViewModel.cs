﻿using Domain.Entity.Enum;
using System;

namespace Domain.Web.Models.Attachment
{
    public class AttachmentViewModel
    {
        public Guid AttachmentId { get; set; }
        public Guid RelatedEntityId { get; set; }
        public EnumRelatedEntity RelatedEntity { get; set; }
    }
}