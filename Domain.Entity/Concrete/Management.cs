using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Concrete
{
    public class Management : IEntity
    {
        [Key]
        public Guid ManagementId { get; set; }

        public string AboutPageContent { get; set; }

        [StringLength(250)]
        public string FirmAddress { get; set; }

        [StringLength(50)]
        public string FirmPhoneNumber { get; set; }

        [StringLength(50)]
        public string FirmEMail { get; set; }
    }
}
