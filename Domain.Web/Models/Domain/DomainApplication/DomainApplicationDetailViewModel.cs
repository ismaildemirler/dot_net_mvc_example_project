using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Domain.DomainApplication
{
    public class DomainApplicationDetailViewModel
    {
        [Required]
        public byte Period { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string DomainName { get; set; }

        [Required]
        public int TLDTypeId { get; set; }

        [Required]
        public int DomainOperationTypeId { get; set; }

        [Required]
        public int DomainStateId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Guid TransferredCustomerId { get; set; }

        public Guid DomainDetailId { get; set; }

        public decimal Price { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string IpAddress { get; set; }

        public string Currency { get; set; }

        public Guid CustomerApplicationId { get; set; }
    }
}