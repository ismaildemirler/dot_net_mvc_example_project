using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Infrastructure.DataAccess.EntityFramework.DbContextBase
{
    [Table("ErrorLog")]
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long ErrorLogId { get; set; }
        public string MethodName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string ServerName { get; set; }
        public DateTime ErrorTime { get; set; }
        public Guid AppId { get; set; }
    }
}
