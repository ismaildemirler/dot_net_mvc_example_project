using System;

namespace Domain.Entity.Container.Request.Manage
{
    public class RequestManagement
    {
        public Guid ManagementId { get; set; }
        public string AboutPageContent { get; set; }
        public string FirmAddress { get; set; }
        public string FirmPhoneNumber { get; set; }
        public string FirmEMail { get; set; }
    }
}
