using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Infrastructure.DataAccess.EntityFramework.DbContextBase
{
    [Table("TableLog")]
    public class TableLog
    {
        public TableLog()
        {
            this.TableLogDetails = new HashSet<TableLogDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long TableLogId { get; set; }
        [Required]
        public Guid AppId { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Required]
        public string TableName { get; set; }
        [Required]
        public string PrimaryKeyValue { get; set; }
        public Guid? OperationGuid { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]

        public string Ip { get; set; }
        [MaxLength(1)]
        [StringLength(1)]
        [Required]
        public string State { get; set; }
        
        public int? ProxyUserId { get; set; }

        public ICollection<TableLogDetail> TableLogDetails { get; set; }
    }
}
