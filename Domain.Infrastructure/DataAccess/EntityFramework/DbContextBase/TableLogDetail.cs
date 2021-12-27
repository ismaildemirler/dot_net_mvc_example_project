using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Infrastructure.DataAccess.EntityFramework.DbContextBase
{
    [Table("TableLogDetail")]
    public class TableLogDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long TableLogDetailId { get; set; }
        [Required]
        public long TableLogId { get; set; }
        [Required]
        public string ColumnName { get; set; }
        [MaxLength]
        public string OldValue { get; set; }
        [MaxLength]
        public string NewValue { get; set; }

        public TableLog TableLog { get; set; }
    }
}
